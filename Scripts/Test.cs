using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private static Vector3 startPos;
    private static GameObject thisGo;
    //public Text text;

    // Start is called before the first frame update
    void Start()
    {
        thisGo = this.gameObject;
        startPos = thisGo.transform.position;
        //text.text = "123";
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    UserManager.Instance.UserController.ChangeHp(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    UserManager.Instance.UserController.ChangeHp(-1);
        //}
    }

    /// <summary>
    /// 更新相机位置
    /// </summary>
    /// <param name="offset">传入一个玩家移动前与移动后的差值</param>
    public static void UpdateCameraPos(Vector3 offset)
    {
        Vector3 vct = thisGo.transform.position;
        vct.x += offset.x;
        if (vct.x > SysDefine.cameraMaxX || vct.x < SysDefine.cameraMinX)
            return;
        thisGo.transform.position = vct;
    }

    public static void ResetPos()
    {
        thisGo.transform.position = startPos;
    }
}
