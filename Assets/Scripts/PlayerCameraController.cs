using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothFollow))]

public class PlayerCameraController : MonoBehaviour {

    [SerializeField]
    float groundedRotationDamping = 3;
    [SerializeField]
    float unGroundedRotationDamping = 0;
    [SerializeField]
    RocketCarManager playerCar;

    SmoothFollow _smoothFollow;

    // Use this for initialization
    void Start () {
        playerCar.Events.RegisterCallback("OnGrounded", OnPlayerGrounded);
        playerCar.Events.RegisterCallback("OnUnGrounded", OnPlayerUnGrounded);
        _smoothFollow = GetComponent<SmoothFollow>();
    }

    void OnPlayerGrounded()
    {
        _smoothFollow.rotationDamping = groundedRotationDamping;
    }

    void OnPlayerUnGrounded()
    {
        _smoothFollow.rotationDamping = unGroundedRotationDamping;
    }
}
