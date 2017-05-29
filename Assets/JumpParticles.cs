using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpParticles : MonoBehaviour
{

    RocketCarManager _manager;
    ParticleSystem _particleSystem;

    // Use this for initialization
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _manager = transform.parent.parent.GetComponent<RocketCarManager>();
        _manager.Events.RegisterCallback("OnJump", OnJump);

    }

    void OnJump()
    {
        _particleSystem.time = 0;
        _particleSystem.Play();
    }
}
