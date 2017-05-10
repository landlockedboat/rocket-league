using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RocketCarJumpController : MonoBehaviour {

    [SerializeField]
    float jumpForce = 500;

    [SerializeField]
    RocketCarManager carManager;

    Rigidbody _rigidbody;

    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
    }
	
    public void ApplyJumpForce()
    {
        // Let us assume perfectly vertical jumps
        Vector3 force = Vector3.up;
        // We want to add the force relative to our
        // vertical axis
        force = transform.rotation * force;
        force *= jumpForce;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
