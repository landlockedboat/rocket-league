using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    AudioClip goalSound;
    AudioSource goalSource;

    [SerializeField]
    AudioClip bgSound;
    AudioSource bgSource;

    GameManager gameManager;

    // Use this for initialization
    void Start () {
        bgSource = InitAudioSource(bgSound);
        bgSource.loop = true;
        bgSource.volume = .1f;
        bgSource.Play();

        goalSource = InitAudioSource(goalSound);
        gameManager = GameManager.Instance;
        gameManager.Events.RegisterCallback("OnRedScored", OnScored);
        gameManager.Events.RegisterCallback("OnBlueScored", OnScored);
        gameManager.Events.RegisterCallback("OnGameOver", OnScored);
        gameManager.Events.RegisterCallback("OnGameResetted", OnGameResetted);

    }

    private AudioSource InitAudioSource(AudioClip clip)
    {
        AudioSource aso = gameObject.AddComponent<AudioSource>();
        aso.clip = clip;
        aso.volume = .5f;
        return aso;
    }

    void OnScored()
    {
        goalSource.time = .1f;
        goalSource.Play();
    }

    void OnGameResetted()
    {
    }
}
