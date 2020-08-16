using System;
using UnityEngine;
using UnityEngine.UI;

//提供访问用户数据和修改用户数据的接口
public class UserManager : BaseManager
{
    private Vector3 startPos;           //出生的位置
    private GameObject _player;

    private static UserManager _Instance;
    public static UserManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new UserManager();
            return _Instance;
        }
    }
    public UserController userController;
    public UserController UserController
    {
        get
        {
            if (userController == null)
                Start();
                //userController = GameObject.Find("player").GetComponent<UserController>();
            return userController;
        }
    }
    //public GameObject canvas;

    private void Start()
    {
        if (userController == null)
        {
            InitUserController();
        }
        UIManager.Instance.player = userController.gameObject.GetComponent<Image>();
    }

    private void InitUserController()
    {
        _player = GameObject.Find("player");
        if (!_player)
        {
            _player = Instantiate(Resources.Load("Prefabs/Player") as GameObject);
            _player.name = "player";
            _player.transform.SetParent(GameManager.Canvas.transform);
            _player.transform.localPosition = new Vector3(SysDefine.Instance.playerStartPosX, SysDefine.Instance.playerStartPosY, 0);
            userController = _player.GetComponent<UserController>();
        }
        else if (userController == null)
        {
            userController = _player.GetComponent<UserController>();
            if (userController == null)
            {
                userController = _player.AddComponent<UserController>();
            }
        }
        startPos = _player.transform.position;
    }

    public void HuiCheng()
    {
        _player = GameObject.Find("player");
        _player.transform.localPosition = new Vector3(SysDefine.Instance.playerStartPosX, SysDefine.Instance.playerStartPosY, _player.transform.position.z);
        Test.ResetPos();
    }

    private void Update()
    {
        //userController.Update(UIManager.Instance.player);
    }

    private UserInfo userInfo = null;
    public UserInfo UserInfo{
        get{
            InitUserController();
            GameObject go = GameObject.Find("player");
            if (userInfo==null&&go!=null){
                userInfo = go.GetComponent<UserInfo>();
                if(userInfo==null)
                {
                    userInfo = go.AddComponent<UserInfo>();
                    userInfo.InitValue(SysDefine.Instance.gameStartPlayerHpValue);
                }
            }
            else
            {
                go = GameObject.Find("Canvas/player");
                if (userInfo == null && go != null)
                {
                    userInfo = go.GetComponent<UserInfo>();
                    if (userInfo == null)
                    {
                        userInfo = go.AddComponent<UserInfo>();
                        userInfo.InitValue(SysDefine.Instance.gameStartPlayerHpValue);
                    }
                }
            }
            return userInfo;
        }
        set
        {
            userInfo=value;
        }
    }

    public UserManager()
    {

    }

    public override void Init(){
        InitConfigDate();
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    public void InitConfigDate()
    {
        if (UserInfo == null)
        {
            //GameObject go = GameObject.Find("player");
            //if (go == null)
            //{
            //    userInfo = go.AddComponent<UserInfo>();
            userInfo.InitValue(SysDefine.Instance.gameStartPlayerHpValue);
            //}
        }
        // ArmsData.Instance.Init();
    }

    public void UpdatePlayerPos(Vector2 playerPos)
    {
        GameObject player = userController.gameObject;
        player.transform.position = new Vector3(playerPos.x, playerPos.y, player.transform.position.z);
    }
}