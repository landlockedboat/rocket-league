using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RocketCarManager))]
public class RocketCarAIControl : MonoBehaviour {

    private RocketCarManager carManager;

    private void Awake()
    {
        // get the car controller
        carManager = GetComponent<RocketCarManager>();
    }

    private void FixedUpdate()
    {
        // TODO
    }

}
