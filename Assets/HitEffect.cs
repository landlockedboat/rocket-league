using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour {

    [SerializeField]
    ParticleSystem _particleSystem;
    [SerializeField]
    AudioClip hitSound;
    AudioSource hitSource;

    [SerializeField]
    float minPitch = .5f;
    [SerializeField]
    float maxPitch = 1;


    private void Start()
    {
        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.clip = hitSound;
        hitSource.spatialBlend = 1;
        hitSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        _particleSystem.transform.position = collision.contacts[0].point;
        _particleSystem.time = 0;
        _particleSystem.Play();

        hitSource.time = 0;
        hitSource.pitch = Mathf.Clamp(Random.value, minPitch, maxPitch);
        if(SceneData.Instance.GetData("sound") == 1)
            hitSource.Play();
    }
}
