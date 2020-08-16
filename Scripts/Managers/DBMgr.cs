using System;

public class DBMgr : BaseManager
{
    private static DBMgr _Instance;
    public static DBMgr Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new DBMgr();
            return _Instance;
        }
    }

    public DBMgr()
    {

    }

    public override void Init(){
        InitConfigDate();
    }

    /// <summary>
    /// 将配置表中的数据读取到内存中
    /// </summary>
    public void InitConfigDate()
    {
        TalkData.Instance.Init();
        NpcData.Instance.Init();
        GameMapData.Instance.Init();

        GameManager.Init();
    }

}