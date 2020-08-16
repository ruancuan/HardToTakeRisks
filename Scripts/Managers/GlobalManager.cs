using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalManager : BaseManager
{
    private static GlobalManager _instance;
    public static GlobalManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public UIManager UIManager;
    public SysDefine sysDefine=null;

    // public CameraController cameraController;
    public enum ManagerType
    {
        None,
        DBMgr,
        UserManager,
        UIManager,
        EventManager,
        ResourcesManager
    }
    public Dictionary<string, BaseManager> dicManager = new Dictionary<string, BaseManager>();
    new private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        InitManager();
        UIManager.Instance = UIManager;
        //限制鼠标不能离开游戏界面
        // Cursor.lockState=CursorLockMode.Confined;
    }

    // Use this for initialization
    new public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new public void Update()
    {
        base.Update();
    }

    void InitManager()
    {
        AddComponentAndAddDic<DBMgr>(ManagerType.DBMgr);
        AddComponentAndAddDic<UserManager>(ManagerType.UserManager);
        //AddComponentAndAddDic<EventManager>(ManagerType.EventManager);
        AddComponentAndAddDic<ResourcesManager>(ManagerType.ResourcesManager);
        //AddComponentAndAddDic<UIManager>(ManagerType.UIManager);

        //需要物体承载的在此处创建
        GameObject obj = new GameObject("Manager");
        foreach (BaseManager mgr in dicManager.Values)
        {
            mgr.transform.SetParent(obj.transform);
            mgr.SetGlobalManager(this);
        }
        UIManager.Instance.Init();

    }


    public void AddComponentAndAddDic<T>(ManagerType type) where T : BaseManager
    {
        string managerName = Enum.GetName(type.GetType(), type);
        GameObject go = new GameObject(managerName);
        T component = go.AddComponent<T>();
        dicManager.Add(managerName, go.GetComponent<T>());
        component.Init();
    }

    public T GetTargetManager<T>(ManagerType type) where T : BaseManager
    {
        BaseManager mgr;
        dicManager.TryGetValue(Enum.GetName(type.GetType(), type), out mgr);
        if (mgr != null)
            return mgr.gameObject.GetComponent<T>();
        return null;
    }



}
