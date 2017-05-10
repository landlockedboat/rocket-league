using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCarRotationController : MonoBehaviour {

    [SerializeField]
    float rotationAmmount = 50f;

    Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ApplyRotation(float h, float v)
    {
        float horRot = rotationAmmount * h * Time.deltaTime;
        float vertRot = rotationAmmount * v * Time.deltaTime;
        _rigidbody.AddTorque(transform.up * horRot, ForceMode.VelocityChange);
        _rigidbody.AddTorque(transform.right * vertRot, ForceMode.VelocityChange);
    }
}
