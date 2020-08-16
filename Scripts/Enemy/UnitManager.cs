using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private static List<GameObject> enemyList = new List<GameObject>();
    private static List<GameObject> npcList = new List<GameObject>();

    private static UnitManager _instance;
    public static UnitManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UnitManager();
            return _instance;
        }
    }
    private static List<string> animationList = new List<string>() { "Anima/man_1", "Anima/old_1", "Anima/woman" };

    //private 

    /// <summary>
    /// 生成一个敌人
    /// </summary>
    public static GameObject CreateEnemy(Transform parent, Vector2 pos)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemy"), parent) as GameObject;
        enemy.transform.position = new Vector3(pos.x, pos.y, enemy.transform.position.z);
        enemyList.Add(enemy);
        return enemy;
    }

    public static GameObject CreateNpc(Transform parent,Vector2 pos,int id)
    {
        GameObject npc = Instantiate(Resources.Load("Prefabs/NPC"), parent) as GameObject;
        npc.transform.position = new Vector3(pos.x, pos.y, npc.transform.position.z);
        //添加npcInfo
        NpcInfo info = npc.AddComponent<NpcInfo>();
        Npc npcInfo= NpcData.Instance.GetNpcById(id);
        Animator animator = npc.GetComponent<Animator>();
        if (!animator)
        {
            animator=npc.AddComponent<Animator>();
        }
        if (animator)
        {
            int tempId = id %3;
            //animator.runtimeAnimatorController = SysDefine.Instance.animatorList[tempId];
            animator.runtimeAnimatorController = Resources.Load<UnityEngine.RuntimeAnimatorController>(animationList[tempId]);
        }
        if (npcInfo!=null)
        {
            info.iconPath = npcInfo.IconPath;
            info.id = id;
            Talk talk = TalkData.Instance.GetTalkById(npcInfo.TaskId);
            if (talk != null)
            {
                info.leftActions = new EM_NPC_ACTION[1];
                info.leftActions[0] = (EM_NPC_ACTION)talk.Choice1Type;
                info.rightActions = new EM_NPC_ACTION[1];
                info.rightActions[0] = (EM_NPC_ACTION)talk.Choice2Type;
            }
        }
        npcList.Add(npc);
        return npc;
    }

    /// <summary>
    /// 销毁敌人
    /// </summary>
    public static void DestoryEnemy(GameObject go)
    {
        if (go != null)
        {
            enemyList.Remove(go);
            Destroy(go);
        }
    }

    /// <summary>
    /// 消灭当前屏幕里所有的敌人
    /// </summary>
    public static void ClearEnemy()
    {
        for (int i = 0; i < enemyList.Count; )
        {
            if (enemyList[i].transform.position.x > -20 && enemyList[i].transform.position.x < Screen.width + 20)
            {
                Destroy(enemyList[i]);
                enemyList.Remove(enemyList[i]);
            }
            else
            {
                i++;
            }
        }
    }

    /// <summary>
    /// 得到指定对象的Enemy内容
    /// </summary>
    /// <param name="enemyGo">敌人的GameObject</param>
    /// <returns></returns>
    public static GameObject GetEnemyInfo(GameObject enemyGo)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == enemyGo)
                return enemyList[i];
        }
        return null;
    }
}
