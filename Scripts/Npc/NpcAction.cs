using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAction : MonoBehaviour
{
    public static void DoLeftAction(NpcInfo info)
    {
        for (int i = 0; i < info.leftActions.Length; i++)
        {
            DoAction(info.leftActions[i]);
        }
    }

    public static void DoRightAction(NpcInfo info)
    {
        for (int i = 0; i < info.rightActions.Length; i++)
        {
            DoAction(info.rightActions[i]);
        }
    }

    private static void DoAction(EM_NPC_ACTION action)
    {
        UserController uc;
        switch (action)
        {
            case EM_NPC_ACTION.ReverseOperation:
                //uc = UIManager.Instance.player.GetComponent<UserController>();
                //uc.yiquanchaoren = true;
                UserManager.Instance.UserController.dir *= -1;
                break;
            case EM_NPC_ACTION.SpeedHalf:
                SysDefine.Instance.playerMoveSpeed /= 2;
                //uc = UIManager.Instance.player.GetComponent<UserController>();
                //uc.fly = true;
                break;
            case EM_NPC_ACTION.GOHOME:
                UserManager.Instance.HuiCheng();
                break;
            case EM_NPC_ACTION.NONE:
                break;
        }
    }
}
