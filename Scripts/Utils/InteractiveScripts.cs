using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 提供交互的脚本,使物体能够与玩家进行交互
/// </summary>
[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class InteractiveScripts : MonoBehaviour
{
    public EM_InteractiveType type = EM_InteractiveType.None;
    // Start is called before the first frame update
    void Start()
    {
        Button btn=this.GetComponent<Button>();
        if (!btn)
        {
            btn = this.gameObject.AddComponent<Button>();
        }
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(ClickCallBack);
            trigger.triggers.Add(entry);
        }
    }

    private void ClickCallBack(BaseEventData pointData)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
