using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {

    static Dictionary<string, Action> callbacks;
    static bool isInit = false;

    static void Init()
    {
        callbacks = new Dictionary<string, Action>();
        isInit = true;
    }

    public static void RegisterCallback(string s, Action a)
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

    public static void UnRegisterCallback(string s, Action a)
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

    public static void TriggerCallback(string s)
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
