using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 取消
/// </summary>
public class EventManager : BaseManager
{
    private Dictionary<EM_EventType, Action<EventParm>> callBackDict;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Init()
    {

    }
}
