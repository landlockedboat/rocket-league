using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothFollow))]

public class PlayerCameraController : MonoBehaviour {

    bool followingPlayer = true;
    bool playerGrounded;

    SmoothFollow _smoothFollow;
    BallCam _ballCam;

    RocketCarManager playerCar;
    Transform ballTransform;

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

        _ballCam = GetComponent<BallCam>();
        _ballCam.Car = playerCar.transform;
        _ballCam.Ball = GameManager.Instance.BallGameObject.transform;

        ballTransform = GameManager.Instance.BallGameObject.transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            followingPlayer = !followingPlayer;
            _smoothFollow.enabled = followingPlayer;
            if(_smoothFollow.enabled)
            {
                _smoothFollow.FollowRotation = playerGrounded;
            }
            _ballCam.enabled = !followingPlayer;           
        }
    }

    void OnPlayerGrounded()
    {
        playerGrounded = true;
        if (followingPlayer)
        {
            _smoothFollow.FollowRotation = playerGrounded;
        }
    }

    void OnPlayerUnGrounded()
    {
        playerGrounded = false;
        if(followingPlayer)
        {
            _smoothFollow.FollowRotation = playerGrounded;
        }
    }
}
