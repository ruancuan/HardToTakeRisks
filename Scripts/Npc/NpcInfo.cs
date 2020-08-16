using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo : MonoBehaviour
{
    public int id;                      //id
    public int map;                     //所在地图Id
    public string iconPath;             //头像路径
    public EM_NPC_ACTION[] leftActions;     //执行左边选择的行为
    public EM_NPC_ACTION[] rightActions;    //执行右边选择的行为
}
