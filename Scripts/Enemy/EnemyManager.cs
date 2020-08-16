using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static List<GameObject> enemyList = new List<GameObject>();

    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new EnemyManager();
            return _instance;
        }
    }

    /// <summary>
    /// 生成一个敌人
    /// </summary>
    public static GameObject CreateEnemy(Transform parent, Vector2 pos)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemy"), parent) as GameObject;
        enemy.transform.position = new Vector3(pos.x, pos.y, enemy.transform.position.z);
        enemyList.Add(enemy);
        return enemy;
    }

    /// <summary>
    /// 销毁敌人
    /// </summary>
    public static void DestoryEnemy(GameObject go)
    {
        if (go != null)
        {
            enemyList.Remove(go);
            Destroy(go);
        }
    }

    /// <summary>
    /// 消灭当前屏幕里所有的敌人
    /// </summary>
    public static void ClearEnemy()
    {
        for (int i = 0; i < enemyList.Count; )
        {
            if (enemyList[i].transform.position.x > -20 && enemyList[i].transform.position.x < Screen.width + 20)
            {
                Destroy(enemyList[i]);
                enemyList.Remove(enemyList[i]);
            }
            else
            {
                i++;
            }
        }
    }

    internal static GameObject CreateNpc(Transform transform, Vector2 vct2, int npcId)
    {
        //throw new NotImplementedException();
        return null;
    }

    /// <summary>
    /// 得到指定对象的Enemy内容
    /// </summary>
    /// <param name="enemyGo">敌人的GameObject</param>
    /// <returns></returns>
    public static GameObject GetEnemyInfo(GameObject enemyGo)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == enemyGo)
                return enemyList[i];
        }
        return null;
    }
}
