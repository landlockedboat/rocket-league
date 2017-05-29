using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour {

    [SerializeField]
    ParticleSystem _particleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        _particleSystem.transform.position = collision.contacts[0].point;
        _particleSystem.time = 0;
        _particleSystem.Play();
    }
}
