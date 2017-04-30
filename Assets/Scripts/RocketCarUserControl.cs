using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(RocketCarController))]
public class RocketCarUserControl : MonoBehaviour
{
    private RocketCarController m_Car; // the car controller we want to use


    private void Awake()
    {
        // get the car controller
        m_Car = GetComponent<RocketCarController>();
    }


    private void FixedUpdate()
    {
        // pass the input to the car!
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
        float handbrake = CrossPlatformInputManager.GetAxis("Fire1");
        m_Car.Move(h, v, v, handbrake);
#else
        m_Car.Move(h, v, v, 0f);
#endif
    }
}
