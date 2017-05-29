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

    [SerializeField]
    GameObject[] visualPrefabs;
    [SerializeField]
    Transform visualPivot;

    Vector3 blueGoal;
    Vector3 redGoal;

    float distanceToRedGoal = float.PositiveInfinity;
    float distanceToBlueGoal = float.PositiveInfinity;

    SphereCollider _sphereCollider;
    GameObject visuals;
    Rigidbody _rigidbody;
    ParticleSystem _particleSystem;

    GameManager gameManager;

    void Start () {
        gameManager = GameManager.Instance;
        if(gameManager)
        {
            blueGoal = gameManager.GetGoalPos(true);
            redGoal = gameManager.GetGoalPos(false);
            InvokeRepeating("UpdateDistanceToGoals", 0, updateDistanceTime);
            gameManager.Events.RegisterCallback("OnBlueScored", OnScored);
            gameManager.Events.RegisterCallback("OnRedScored", OnScored);
            gameManager.Events.RegisterCallback("OnGameResetted", OnGameResetted);
            gameManager.Events.RegisterCallback("OnGameOver", OnGameOver);
        }
        
        _sphereCollider = GetComponent<SphereCollider>();
        visuals = transform.GetChild(0).gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponent<ParticleSystem>();

        int randomVisualIndex = Random.Range(0, visualPrefabs.Length);

        Instantiate(visualPrefabs[randomVisualIndex], visualPivot.position,
            visualPivot.rotation, visualPivot);

    }

    void OnScored()
    {
        _sphereCollider.enabled = false;
        visuals.SetActive(false);
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
        visuals.SetActive(true);
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
