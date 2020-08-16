using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : BasePanel
{
    public Text taskContent;
    public HorizontalLayoutGroup group;
    private List<ChooseItem> itemList = new List<ChooseItem>();
    private EM_SignChooseType playerChoose;
    public Image NpcIcon;
    //public GameObject arrow;
    private int npcId;
    private Action<EM_SignChooseType> action=null;

    /// <summary>
    /// 初始化面板数据
    /// </summary>
    public override void InitPanel(Action<EM_SignChooseType> callBack, OpenWinParm parm)
    {
        if (group != null && parm != null)
        {
            winType = EM_WinType.DialoguePanel;
            GameObject item = Resources.Load("Prefabs/ChosePrefab") as GameObject;
            npcId = parm.npcId;
            Npc npcInfo = NpcData.Instance.GetNpcById(npcId);
            Talk talkInfo = TalkData.Instance.GetTalkById(npcInfo.TaskId);
            if (talkInfo!=null)
            {
                UpdateContent(talkInfo.Content);
                //添加选择进去
                for (int i = 0; i < SysDefine.Instance.DialogueNum; i++)
                {
                    GameObject go = Instantiate(item);
                    ChooseItem choose = go.GetComponent<ChooseItem>();
                    if (!choose)
                    {
                        choose = go.AddComponent<ChooseItem>();
                    }
                    action = callBack;
                    choose.InitContent(i == 0 ? EM_SignChooseType.Left : EM_SignChooseType.Right, CallBack);
                    //choose.InitContent(i == 0 ? EM_SignChooseType.Left : EM_SignChooseType.Right, (EM_SignChooseType choose,int type)=> { 
                    choose.UpdateContent(i == 0 ? talkInfo.Choice1Content : talkInfo.Choice2Content);
                    choose.SetFunctionType(i == 0 ? talkInfo.Choice1Type : talkInfo.Choice2Type);
                    //});
                    go.transform.SetParent(group.transform);
                    itemList.Add(choose);
                }
                group.spacing = SysDefine.Instance.DialogueSpacing;
            }
        }
    }

    private void InitPanelInfo()
    {

    }

    private void CallBack(EM_SignChooseType choose, int type)
    {
        playerChoose = choose;
        if (action!=null)
        {
            action(choose);
        }
        UpdateData();
    }

    //public void 

    public void UpdateContent(string content)
    {
        taskContent.text = content;
    }

    /// <summary>
    /// 当玩家做出选择时进行的刷新
    /// </summary>
    private void UpdateData()
    {
        //关闭面板
        ClosePanel();
    }
}
