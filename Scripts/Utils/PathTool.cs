using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathTool
{
    private static PathTool _Instance;
    public static PathTool Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new PathTool();
            return _Instance;
        }
    }

    private Dictionary<PathType, string> pathDic = new Dictionary<PathType, string>();

    public PathTool()
    {
        InitPathDic();
    }

    void InitPathDic()
    {
        pathDic.Add(PathType.ConfigTable, "Resources/Configure");
    }

    public string GetPathByType(PathType type)
    {
        string result = "";
        if (pathDic.ContainsKey(type))
        {
            pathDic.TryGetValue(type, out result);
        }
        return result;
    }

    public enum PathType
    {
        ConfigTable
    }

    public void OnRelease()
    {
        pathDic.Clear();
    }
}

