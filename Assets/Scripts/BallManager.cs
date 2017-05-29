using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    [SerializeField]
    float updateDistanceTime = .2f;
    [SerializeField]
    float explosionRadius = 200000f;
    [SerializeField]
    float explosionForce = 50000;
    [SerializeField]
    float jumpForce = 20000;

    Vector3 blueGoal;
    Vector3 redGoal;

    float distanceToRedGoal = float.PositiveInfinity;
    float distanceToBlueGoal = float.PositiveInfinity;

    SphereCollider _sphereCollider;
    MeshRenderer _renderer;
    Rigidbody _rigidbody;
    ParticleSystem _particleSystem;

    void Start () {
        blueGoal = GameManager.Instance.GetGoalPos(true);
        redGoal = GameManager.Instance.GetGoalPos(false);
        InvokeRepeating("UpdateDistanceToGoals", 0, updateDistanceTime);
        GameManager.Instance.Events.RegisterCallback("OnBlueScored", OnScored);
        GameManager.Instance.Events.RegisterCallback("OnRedScored", OnScored);
        GameManager.Instance.Events.RegisterCallback("OnGameResetted", OnGameResetted);
        GameManager.Instance.Events.RegisterCallback("OnGameOver", OnGameOver);

        _sphereCollider = GetComponent<SphereCollider>();
        _renderer = GetComponentInChildren<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void OnScored()
    {
        _sphereCollider.enabled = false;
        _renderer.enabled = false;
        _rigidbody.isKinematic = true;
        _particleSystem.Play();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Car");

        foreach (GameObject go in gos)
        {
            Rigidbody r = go.GetComponent<Rigidbody>();

            Vector3 force = Vector3.up;
            force = transform.rotation * force;
            force *= jumpForce;
            r.AddForce(force, ForceMode.Impulse);

            r.AddExplosionForce(explosionForce,
                transform.position, explosionRadius);
        }
    }

    void OnGameOver()
    {
        _rigidbody.isKinematic = true;
    }

    void OnGameResetted()
    {
        _sphereCollider.enabled = true;
        _renderer.enabled = true;
        _rigidbody.isKinematic = false;
        _particleSystem.Stop();
        _particleSystem.time = 0;
    }



    void UpdateDistanceToGoals()
    {
        distanceToBlueGoal = Vector3.Distance(transform.position, blueGoal);
        distanceToRedGoal = Vector3.Distance(transform.position, redGoal);
    }

    public float GetDistanceToGoal(bool isBlue)
    {
        if(isBlue)
        {
            return distanceToBlueGoal;
        }
        else
        {
            return distanceToRedGoal;
        }
    }
}
