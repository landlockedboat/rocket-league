using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCarAudio : MonoBehaviour {
    [SerializeField]
    AudioClip accelSound;
    AudioSource accelSource;
    [SerializeField]
    AudioClip boostSound;
    AudioSource boostSource;

    bool isAccelerating = false;
    bool isBoosting = false;

    RocketCarManager _manager;

    // Use this for initialization
    void Start () {
        _manager = GetComponent<RocketCarManager>();
        _manager.Events.RegisterCallback("OnAccelerationBegin", OnAccelerationBegin);
        _manager.Events.RegisterCallback("OnAccelerationEnd", OnAccelerationEnd);
        _manager.Events.RegisterCallback("OnBoostBegin", OnBoostBegin);
        _manager.Events.RegisterCallback("OnBoostEnd", OnBoostEnd);

        accelSource = InitAudioSource(accelSound);

        boostSource = InitAudioSource(boostSound);
    }

    private AudioSource InitAudioSource(AudioClip clip)
    {
        AudioSource aso = gameObject.AddComponent<AudioSource>();
        aso.clip = clip;
        aso.loop = true;
        aso.Stop();
        aso.spatialBlend = 1;
        return aso;
    }



    void OnAccelerationBegin()
    {
        accelSource.Play();
    }

    void OnAccelerationEnd()
    {
        accelSource.Stop();
    }

    void OnBoostBegin()
    {
        boostSource.Play();
    }

    void OnBoostEnd()
    {
        boostSource.Stop();
    }
}
