using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour {
    [SerializeField]
    RocketCarManager _carManager;
    [SerializeField]
    TrailRenderer[] trailRenderers;

    private void Start()
    {
        _carManager.Events.RegisterCallback("OnBoostBegin", OnBoostBegin);
        _carManager.Events.RegisterCallback("OnBoostEnd", OnBoostEnd);
    }

    private void OnBoostBegin()
    {
        foreach(TrailRenderer t in trailRenderers)
        {
            t.enabled = true;
            t.Clear();
        }
    }

    private void OnBoostEnd()
    {
        foreach (TrailRenderer t in trailRenderers)
        {
            t.enabled = false;
        }
    }

    void Update () {
		
	}
}
