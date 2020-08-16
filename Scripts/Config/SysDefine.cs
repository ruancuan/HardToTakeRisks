using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存放定义的常量
/// </summary>
public class SysDefine:MonoBehaviour
{
    [Header("初始血量")]
    public int gameStartPlayerHpValue=3;
    //[Header("血量槽位置")]
    //public int[] hpGroupPos= new int[2]{-548,360};
    [Header("对话框位置")]
    public int[] talkGroupPos= new int[2]{25,0};
    [Header("玩家移动速度")]
    public float playerMoveSpeed = 100f;
    [Header("敌人移动速度")]
    public float enemyMoveSpeed = 50f;
    [Header("玩家位置约束")]
    public float playerPosXMin = -434;
    public float playerPoxXMax = 437;
    public float playerPosYMin = -260;
    public float playerPosYMax = -178;
    [Header("玩家初始位置")]
    public float playerStartPosX = -475;
    public float playerStartPosY = -115;
    [Header("选择光标位置")]
    public Vector3 leftPos=Vector3.zero;
    public Vector3 rightPos = Vector3.zero;
    [Header("对话框的高度偏差")]
    public float scaleHeight = 20;
    [Header("对话框之间的间距")]
    public float DialogueSpacing = -500;
    [Header("对话框的数量")]
    public int DialogueNum = 2;
    [Header("面板路径")]
    public string SYS_PATH_UI = @"/Resources/Json/PanelPath.json";
    [Header("相机X的最大值")]
    public const float cameraMaxX = 5589;
    [Header("相机X的最小值")]
    public const float cameraMinX = 289;

    private static SysDefine _Instance;
    public static SysDefine Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = GlobalManager.Instance.sysDefine;
            return _Instance;
        }
    }

    //public List<AnimatorController> animatorList;
}

public class OpenWinParm
{
    public int npcId;

    public OpenWinParm(int id)
    {
        npcId = id;
    }
}

public enum EM_WinType
{
    None,
    Die,
    Start,
    DialoguePanel
}

public enum EM_InteractiveType
{
    None,
    Talk,
    Look
}

public enum EM_SignChooseType
{
    None,
    Left,
    Right
}

public enum EM_EventType
{
    None,
}

public enum EM_NPC_TYPE
{
    NULL = 0,
    NPC_1 = 1,
    NPC_2 = 2,
    NPC_3 = 3,
}

public enum EM_NPC_ACTION
{
    NONE,       // 什么卵都不做
    GOHOME,     // 回家
    SpeedHalf,        // 速度减慢
    ReverseOperation     // 操作反向

}

public class EventParm
{
    //public 
}

public enum EM_AnimationType
{
    None,
    Idle,
    Attack,
    Move
}

[SerializeField]
public class KeyValueInfo
{
    public KeyValueNode[] infoList { get; set; }
    public int count { get; set; }
}

[SerializeField]
public class KeyValueNode
{
    public string key { get; set; }
    public string value { get; set; }
}
