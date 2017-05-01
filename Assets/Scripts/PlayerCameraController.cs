using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothFollow))]

public class PlayerCameraController : MonoBehaviour {

    [SerializeField]
    float groundedRotationDamping = 3;
    [SerializeField]
    float unGroundedRotationDamping = 0;

    SmoothFollow _smoothFollow;

    // Use this for initialization
    void Start () {
        EventManager.RegisterCallback("OnPlayerGrounded", OnPlayerGrounded);
        EventManager.RegisterCallback("OnPlayerUnGrounded", OnPlayerUnGrounded);
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
