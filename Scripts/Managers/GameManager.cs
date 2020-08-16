using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject _canvas;
    public static GameObject Canvas{
        get{
            if(_canvas==null)
            {
                _canvas = GameObject.Find("Canvas");
            }
            return _canvas;
        }
    }

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas");
    }

    public static void Init()
    {
        GameMap gm = GameMapData.Instance.GetGameMapById(1);
        print(gm);
        string monsterPos = gm.MonsterPos;
        string[] mpos = monsterPos.Split(';');
        for (int i = 0; i < mpos.Length; i++)
        {
            string[] pos = mpos[i].Split('_');

            Vector2 vct2 = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));
            GameObject enemy = UnitManager.CreateEnemy(Canvas.transform, vct2);
            //GameObject enemy = EnemyManager.CreateEnemy(Canvas.transform, vct2);
            //后续可以添加怪物属性
        }
        string npcPos = gm.NpcPos;
        mpos = npcPos.Split(';');
        string[] ids = gm.NpcId.Split(';');
        for (int i = 0; i < mpos.Length; i++)
        {
            string[] pos = mpos[i].Split('_');

            Vector2 vct2 = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));
            GameObject enemy = UnitManager.CreateNpc(Canvas.transform, vct2, int.Parse(ids[i]));
            //GameObject enemy = EnemyManager.CreateNpc(Canvas.transform, vct2, gm.NpcId);
            //GameObject npc=
        }
    }
}
