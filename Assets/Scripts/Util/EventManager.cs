using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {

    Dictionary<string, Action> callbacks;
    bool isInit = false;

    void Init()
    {
        callbacks = new Dictionary<string, Action>();
        isInit = true;
    }

    public void RegisterCallback(string s, Action a)
    {
        if (!isInit)
            Init();

        if (callbacks.ContainsKey(s))
        {
            callbacks[s] += a;
        }
        else
        {
            Debug.Log("RegisterCallback: Callback " + s + " does not exist. \n" +
                "Creating new one.");
            callbacks.Add(s, a);
        }
    }

    public void UnRegisterCallback(string s, Action a)
    {
        if (!isInit)
            Init();

        if (callbacks.ContainsKey(s))
        {
            callbacks[s] -= a;
        }
        else
        {
            Debug.Log("UnRegisterCallback: Callback " + s + " does not exist.");
        }
    }

    public void TriggerCallback(string s)
    {
        if (!isInit)
            Init();

        Debug.Log("TriggerCallback: " + s);

        if (callbacks.ContainsKey(s))
        {
            callbacks[s]();
        }
        else
        {
            Debug.Log("TriggerCallback: Callback " + s + " does not exist.");
        }
    }

}
