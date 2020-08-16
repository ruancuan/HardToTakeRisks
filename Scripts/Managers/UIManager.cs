using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//提供窗口变换/弹窗的接口
public class UIManager : BaseManager
{
    private static UIManager _Instance;
    public static UIManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new UIManager();
            return _Instance;
        }
        set
        {
            _Instance = value;
        }
    }

    public HorizontalLayoutGroup group;
    private GameObject hpPrefabs;
    public Image player;
    public Sprite fullHp;
    public Sprite nullHp;
    /// <summary>
    /// 存放路径的字典
    /// </summary>
    private Dictionary<string, string> dicPath = new Dictionary<string, string>();
    private Dictionary<EM_WinType, GameObject> panelDict = new Dictionary<EM_WinType, GameObject>();

    public void AddHp(int num)
    {
        int hp = UserManager.Instance.UserInfo.hp;
        if (hp >= 3)
        {
        }
        else if (hp >= 1&& fullHp)
        {
            for (int i = 0; i < num && hp + i <= 2; i++)
            {
                Transform transform= group.transform.GetChild(hp + i);
                if (transform)
                {
                    Image image= transform.GetComponent<Image>();
                    if (image)
                    {
                        image.sprite = fullHp;
                        UserManager.Instance.UserInfo.hp += 1;
                    }
                }
            }
        }
    }
    //private Dictionary<string, string> dicPanel = new Dictionary<string, string>();

    public void ReduceHp(int num)
    {
        if (Mathf.Abs(num) <= 0)
            return;
        int hp = UserManager.Instance.UserInfo.hp;
        if (hp <= 0)
        {
            Debug.LogError("血量扣除错误");
            return;
        }
        else if (hp == 1)
        {
            // 执行死亡 回城
            //UserManager.Instance.UserInfo.hp=3;
            AddHp(2);
            UserManager.Instance.HuiCheng();
            UserManager.Instance.UserController.Died();
        }
        else
        {
            Transform transform = group.transform.GetChild(hp-1);
            if (transform)
            {
                Image image = transform.GetComponent<Image>();
                if (image)
                {
                    image.sprite = nullHp;
                }
            }
            UserManager.Instance.UserInfo.hp = UserManager.Instance.UserInfo.hp- 1;
            ReduceHp(Mathf.Abs(num) - 1);
        }
    }

    private void InitDict()
    {
        dicPath = ResourcesManager.Instance.GetStrStrDic(SysDefine.Instance.SYS_PATH_UI);
    }

    public void ShowAlert(EM_WinType type, Action<EM_SignChooseType> callBack, OpenWinParm parm)
    {
        if (type == EM_WinType.DialoguePanel)
        {
            GameObject go = null;
            //TODO 去面板字典中去获取
            string panelName = Enum.GetName(type.GetType(), type);
            string path = "";
            InitDict();
            if (dicPath.TryGetValue(panelName, out path))
            {
                go = ResourcesManager.Instance.LoadAsset(path, false);
                go.transform.SetParent(GameManager.Canvas.transform);
                go.transform.localPosition = Vector3.zero;
                if (go)
                {
                    DialoguePanel panel = go.GetComponent<DialoguePanel>();
                    if (panel)
                    {
                        panel.InitPanel(callBack, parm);
                        GameObject player= GameObject.Find("Canvas/player");
                        if (player)
                        {
                            panel.transform.SetParent(player.transform);
                            int[] array = SysDefine.Instance.talkGroupPos;
                            panel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(array[0], array[1], 0);
                        }
                    }
                }
            }
            if (go)
            {
                if(!panelDict.ContainsKey(type))
                    panelDict.Add(type, go);
            }
        }
        else if (type == EM_WinType.Die)
        {

        }
    }

    public void CloseAlert(EM_WinType type)
    {
        if (type == EM_WinType.None)
            return;
        GameObject panel = null;
        panelDict.TryGetValue(type, out panel);
        if (panel)
        {
            Destroy(panel);
            panelDict.Remove(type);
        }
    }

    public override void Init()
    {
        //InitDict();
        InitData();
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    public void InitData()
    {
        //canvas = GameObject.Find("Canvas").gameObject;
        if (!group)
        {
            GameObject go = GameObject.Find("Canvas/HpGroup");
            if (go)
            {
                group = go.GetComponent<HorizontalLayoutGroup>();
                if (!group)
                {
                    group = go.AddComponent<HorizontalLayoutGroup>();
                }
            }
            //int[] posArray = SysDefine.Instance.hpGroupPos;
            //if (posArray.Length >= 2)
            //{
            //    group.transform.localPosition = new Vector3(posArray[0], posArray[1], 0);
            //}
        }
        hpPrefabs = (GameObject)Resources.Load("Prefabs/HpPrefab");
        InitHpGroup();
        InitHpNum();
    }

    private void InitHpGroup()
    {
        //int[] pos = SysDefine.hpGroupPos;
        //group.spacing = 10;
        //group.transform.parent = canvas.transform;
        //group.transform.localPosition = new Vector3(pos[0], pos[1], 0);
        RectTransform rt = group.gameObject.GetComponent<RectTransform>();
        player = GameObject.Find("player").GetComponent<Image>();
        group.transform.SetParent(player.transform);
        rt.anchoredPosition3D = new Vector3(50, 25, 0);
        //rt.position = new Vector3(0, 0, 0);
    }

    private void InitHpNum()
    {
        if (hpPrefabs)
        {
            //group.wid = imgWidth * StartNum;
            for (int i = 0; i < UserManager.Instance.UserInfo.hp; i++)
            {
                GameObject temp = GameObject.Instantiate(hpPrefabs);
                temp.transform.SetParent(group.transform);
            }
            group.SetLayoutHorizontal();
        }
    }


    public bool IsOpenWindow(EM_WinType type)
    {
        panelDict.TryGetValue(type, out GameObject panel);
        return panel != null;
    }
}