using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothFollow))]

public class PlayerCameraController : MonoBehaviour {

    SmoothFollow _smoothFollow;
    RocketCarManager playerCar;

    // Use this for initialization
    void Start () {
        playerCar = 
            FindObjectOfType<RocketCarPlayerControl>().
            gameObject.GetComponent<RocketCarManager>();
        playerCar = playerCar.GetComponent<RocketCarManager>();
        playerCar.Events.RegisterCallback("OnGrounded", OnPlayerGrounded);
        playerCar.Events.RegisterCallback("OnUnGrounded", OnPlayerUnGrounded);
        _smoothFollow = GetComponent<SmoothFollow>();
        _smoothFollow.Target = playerCar.transform;
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
