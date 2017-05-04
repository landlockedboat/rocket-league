using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothFollow))]

public class PlayerCameraController : MonoBehaviour {

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
        _smoothFollow.FollowRotation = true;
    }

    void OnPlayerUnGrounded()
    {
        _smoothFollow.FollowRotation = false;
    }
}
