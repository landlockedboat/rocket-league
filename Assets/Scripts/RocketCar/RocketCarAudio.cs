using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCarAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip accelSound;
    AudioSource accelSource;
    [SerializeField]
    AudioClip boostSound;
    AudioSource boostSource;
    [SerializeField]
    AudioClip jumpSound;
    AudioSource jumpSource;

    bool isAccelerating = false;
    bool isBoosting = false;

    RocketCarManager _manager;

    // Use this for initialization
    void Start()
    {
        _manager = GetComponent<RocketCarManager>();
        _manager.Events.RegisterCallback("OnAccelerationBegin", OnAccelerationBegin);
        _manager.Events.RegisterCallback("OnAccelerationEnd", OnAccelerationEnd);
        _manager.Events.RegisterCallback("OnBoostBegin", OnBoostBegin);
        _manager.Events.RegisterCallback("OnBoostEnd", OnBoostEnd);
        _manager.Events.RegisterCallback("OnJump", OnJump);

        accelSource = InitAudioSource(accelSound);

        boostSource = InitAudioSource(boostSound);

        jumpSource = InitAudioSource(jumpSound);
        jumpSource.loop = false;
        jumpSource.spatialBlend = .5f;
    }

    private AudioSource InitAudioSource(AudioClip clip)
    {
        AudioSource aso = gameObject.AddComponent<AudioSource>();
        aso.clip = clip;
        aso.loop = true;
        aso.spatialBlend = 1;
        aso.Stop();
        return aso;
    }



    void OnAccelerationBegin()
    {
        if (SceneData.Instance.GetData("sound") == 1)
            accelSource.Play();
    }

    void OnAccelerationEnd()
    {
        accelSource.Stop();
    }

    void OnBoostBegin()
    {
        if (SceneData.Instance.GetData("sound") == 1)
            boostSource.Play();
    }

    void OnBoostEnd()
    {
        boostSource.Stop();
    }

    void OnJump()
    {
        if (SceneData.Instance.GetData("sound") == 1)
            jumpSource.Play();
    }
}
