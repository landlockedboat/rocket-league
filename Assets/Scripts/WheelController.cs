using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

    [SerializeField]
    GameObject wheelPrefab;
    [SerializeField]
    Transform[] wheelPivots;

	void Start () {
		// We instantiate the wheels for the car
        InstantiateWheels();
	}

    void InstantiateWheels()
    {
        if (wheelPivots.Length != 4)
        {
            Debug.LogError("WheelController: A car can only have four" +
                "wheels");
            return;
        }
        foreach (Transform wheelPivot in wheelPivots)
        {
            // For each wheel pivot we instantiate a wheel and
            // set it as a child of ours.
            GameObject wheelObj =  Instantiate(wheelPrefab, 
                wheelPivot.position,
                Quaternion.identity, transform);

            wheelObj.name = wheelPivot.name;
            // Destroy happens right after the last Update method 
            // has been called so we are in no risk of effing up the
            // loop.
            Destroy(wheelPivot.gameObject);
        }
    }
}
