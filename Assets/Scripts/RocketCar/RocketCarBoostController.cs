using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class RocketCarBoostController : MonoBehaviour
{

    [SerializeField]
    float boostForce = 500;

    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Boost()
    {
        Vector3 force = Vector3.forward;
        force = transform.rotation * force;
        force *= boostForce;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
