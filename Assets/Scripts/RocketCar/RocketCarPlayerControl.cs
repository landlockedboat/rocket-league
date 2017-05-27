using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(RocketCarManager))]
public class RocketCarPlayerControl : MonoBehaviour
{
    private RocketCarManager carManager;

    private void Awake()
    {
        // get the car controller
        carManager = GetComponent<RocketCarManager>();
    }


    private void FixedUpdate()
    {
        // pass the input to the car!
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        // float handbrake = CrossPlatformInputManager.GetAxis("Fire1");
        // carManager.Move(h, v, v, handbrake);
        bool boostB = CrossPlatformInputManager.GetButton("Fire1");
        bool jumpB = CrossPlatformInputManager.GetButtonDown("Fire2");

        carManager.TreatInput(h, v, 0f, jumpB, boostB);
    }
}
