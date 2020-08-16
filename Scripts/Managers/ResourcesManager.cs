using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesManager : BaseManager
{
    public GameObject player;

    private static ResourcesManager _instance;

    public static ResourcesManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ResourcesManager();
            return _instance;
        }
    }
    private Hashtable hashtable = new Hashtable();

    public Dictionary<string, string> GetStrStrDic(string path)
    {
        Dictionary<string, string>  dicPath = new Dictionary<string, string>();
        KeyValueInfo info = null;
        if (string.IsNullOrEmpty(path))
            return null;
        try
        {
            StreamReader streamReader = new StreamReader(Application.dataPath + path);
            JsonData dataJson = streamReader.ReadToEnd();
            info = JsonMapper.ToObject<KeyValueInfo>(dataJson.ToString());
        }
        catch (Exception e)
        {
            throw e;
        }

        foreach (KeyValueNode item in info.infoList)
        {
            if(!dicPath.ContainsKey(item.key))
                dicPath.Add(item.key, item.value);
        }
        return dicPath;
    }

    public GameObject LoadAsset(string path, bool isCatch)
    {
        GameObject obj = LoadResource<GameObject>(path, isCatch);
        GameObject objClone = Instantiate(obj);
        if (objClone == null)
            Debug.LogError("Error LoadAsset " + path);
        return objClone;
    }

    public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(path) || string.Equals(path, "null"))
            return null;
        if (hashtable.Contains(path))
            return hashtable[path] as T;
        T resource = Resources.Load<T>(path);
        if (resource == null)
            Debug.LogError("Error LoadResource " + path);
        if (isCatch)
            hashtable.Add(path, resource);
        return resource;
    }
}
