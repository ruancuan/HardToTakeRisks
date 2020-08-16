using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public EM_WinType winType = EM_WinType.None;
    public virtual void InitPanel(Action<EM_SignChooseType> callBack, OpenWinParm parm)
    {

    }

    public void ClosePanel()
    {
        UIManager.Instance.CloseAlert(winType);
    }
}
