using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    //状态
    public bool yiquanchaoren = false;  //一拳杀全屏
    public bool fly = false;            //开启WS

    private GameObject _player;
    private bool _isMeetNpc = false;
    private GameObject _curNPC;
    private Animator anim;
    private string oldAnimation="";

    //攻击
    private bool _isAttack = false;
    private float _attackTime = 0;

    public int dir = 1;

    private void Awake()
    {
        _player = this.gameObject;
        anim = GetComponent<Animator>();
        //anim.runtimeAnimatorController = Resources.Load("../Anima/player") as RuntimeAnimatorController;
    }

    private void PlayAnimation(EM_AnimationType type,bool b)
    {
        if (anim)
        {
            //enum.get
            string animationType= Enum.GetName(type.GetType(), type);
            print(animationType);
            oldAnimation = animationType;
            //if (oldAnimation != "")
            //{
            //    anim.SetBool(oldAnimation, false);
            //    anim.SetBool(animationType,true);
            //    oldAnimation = animationType;
            //}
            switch (animationType)
            {
                case "Idle":
                    anim.SetBool("isBattle", false);
                    anim.SetBool("isWalk", false);
                    break;
                case "Attack":
                    anim.SetBool("isBattle", true);
                    break;
                case "Move":
                    anim.SetBool("isWalk", true);
                    break;
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (_isAttack)
        {
            _attackTime += Time.deltaTime;
            if (_attackTime > 0.5)
            {
                TransIdeState();
            }
        }

        float H = Input.GetAxis("Horizontal");
        float V = fly ? Input.GetAxis("Vertical") : 0;

        if (H != 0 || V != 0)
        {
            // 加位置限制   Mathf.Clamp()
            Vector3 originPos = _player.transform.localPosition;
            Vector3 pos = _player.transform.localPosition + new Vector3(H, V, 0) * Time.deltaTime * SysDefine.Instance.playerMoveSpeed*dir;
            //pos.x = Mathf.Clamp(pos.x, SysDefine.Instance.playerPosXMin, SysDefine.Instance.playerPoxXMax);
            //pos.y = Mathf.Clamp(pos.y, SysDefine.Instance.playerPosYMin, SysDefine.Instance.playerPosYMax);
            _player.transform.localPosition = pos;
            Vector3 offset = new Vector3(pos.x - originPos.x, pos.y - originPos.y, pos.z - originPos.z);
            Test.UpdateCameraPos(offset);

            string animationType = Enum.GetName(EM_AnimationType.Move.GetType(), EM_AnimationType.Move);
            if (animationType!=oldAnimation)
                PlayAnimation(EM_AnimationType.Move, true);
        }
        else
        {
            string animationType = Enum.GetName(EM_AnimationType.Idle.GetType(), EM_AnimationType.Idle);
            if (animationType != oldAnimation)
                PlayAnimation(EM_AnimationType.Idle, true);
            //PlayAnimation(EM_AnimationType.Idle, true);
        }

        if (Input.GetKeyDown(KeyCode.F) && !_isAttack)
        {
            if (_isMeetNpc)
            {
                if (UIManager.Instance.IsOpenWindow(EM_WinType.DialoguePanel))
                    return;

                NpcInfo npcInfo = _curNPC.GetComponent<NpcInfo>();
                UIManager.Instance.ShowAlert(EM_WinType.DialoguePanel, (EM_SignChooseType type) => {
                    print("do action");
                    switch (type)
                    {
                        case EM_SignChooseType.Left:
                            print("do left");
                            NpcAction.DoLeftAction(npcInfo);
                            break;
                        case EM_SignChooseType.Right:
                            print("do right");
                            NpcAction.DoRightAction(npcInfo);
                            break;
                    }
                }, new OpenWinParm(npcInfo.id));
            }
            else
            {
                Attack();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string otag = other.gameObject.tag;
        switch (otag)
        {
            case "enemy":
                if (_isAttack)
                {
                    // FuckEnemy ,销毁敌人
                    Destroy(other.gameObject);
                }
                else
                {
                    BeAttack();
                }
                break;
            case "npc":
                _isMeetNpc = true;
                _curNPC = other.gameObject;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string otag = other.gameObject.tag;
        switch (otag)
        {
            case "npc":
                _isMeetNpc = false;
                break;
        }
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public void Attack()
    {
        // 攻击时无法二次攻击
        if (_isAttack)
            return;
        string animationType = Enum.GetName(EM_AnimationType.Attack.GetType(), EM_AnimationType.Attack);
        if (animationType != oldAnimation)
            PlayAnimation(EM_AnimationType.Attack, true);
        print("发起攻击");
        if (yiquanchaoren)
        {
            UnitManager.ClearEnemy();
            //EnemyManager.ClearEnemy();
            return;
        }
        _isAttack = true;

        BoxCollider bc = _player.GetComponent<BoxCollider>();
        Vector3 size = bc.size;
        size.x = size.x * 1.5f;
        bc.size = size;
    }

    /// <summary>
    /// 被攻击
    /// </summary>
    /// <param name="loseHp">掉多少血</param>
    public void BeAttack(int loseHp = -1)
    {
        print("扣1血");
        ChangeHp(loseHp);
    }

    /// <summary>
    /// 死去
    /// </summary>
    public void Died()
    {
        //TODO 播放死亡动画

        UIManager.Instance.ShowAlert(EM_WinType.Die, (EM_SignChooseType type) => { }, null);

    }

    /// <summary>
    /// 切换成待机状态，等待下一个状态的触发
    /// </summary>
    private void TransIdeState()
    {

        PlayAnimation(EM_AnimationType.Idle, true);
        _isAttack = false;
        _attackTime = 0;

        BoxCollider bc = _player.GetComponent<BoxCollider>();
        Vector3 size = bc.size;
        size.x = size.x / 1.5f;
        bc.size = size;
    }

    public void ChangeHp(int hpValue)
    {
        if (hpValue > 0)
        {
            UIManager.Instance.AddHp(hpValue);
        }
        else
        {
            UIManager.Instance.ReduceHp(-hpValue);
        }
    }

}
