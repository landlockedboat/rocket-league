using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour {

    private static SceneData instance;

    private Dictionary<string, int> sceneDataDict;

    public static SceneData Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<SceneData>();
                if(!instance)
                {
                    GameObject go = new GameObject();
                    go.name = "SceneData";
                    instance = go.AddComponent<SceneData>();
                }
            }
            return instance;
        }
    }

    Dictionary<string, int> SceneDataDict
    {
        get
        {
            if (sceneDataDict == null)
                sceneDataDict = new Dictionary<string, int>();
            return sceneDataDict;
        }

    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetData(string key, int value)
    {
        if (!SceneDataDict.ContainsKey("key"))
        {
            SceneDataDict.Add(key, value);
        }
        else
        {
            SceneDataDict[key] = value;
        }
    }

    public int GetData(string key)
    {
        if (!SceneDataDict.ContainsKey(key))
        {
            Debug.LogWarning("No key " + key);
            SceneDataDict.Add(key, -1);
        }
        return SceneDataDict[key];
    }
}
