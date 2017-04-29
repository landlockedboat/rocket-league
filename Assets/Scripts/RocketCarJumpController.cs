using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RocketCarJumpController : MonoBehaviour {

    [SerializeField]
    float jumpForce = 500;

    Rigidbody _rigidbody;

    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate () {
        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            // Let us assume perfectly vertical jumps
            Vector3 force = Vector3.up;
            force *= jumpForce;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
	}
}
