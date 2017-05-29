using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RocketCarMotorController))]
[RequireComponent(typeof(RocketCarBoostController))]
[RequireComponent(typeof(RocketCarJumpController))]
[RequireComponent(typeof(RocketCarRotationController))]

public class RocketCarManager : MonoBehaviour
{
    [SerializeField]
    bool isBlueTeam = false;
    [Header("Boost properties")]
    [SerializeField]
    float maxBoostTime = 100f;
    [SerializeField]
    float startingBoostTime = 5f;
    float currentBoostTime;

    bool isBoosting = false;
    bool isAccelerating = false;

    [Header("Object references")]
    [SerializeField]
    GameObject[] carBodyPrefabs;
    [SerializeField]
    int carVisualsIndex = 0;

    GameManager gameManager;

    GameObject carBody;

    // For ground detection
    float closeEnoughForGroundDetection = 1f;
    string floorTag = "Floor";
    bool isGrounded;

    bool canDoSecondJump = true;

    // Triggered as soon as the game starts
    bool canMove = false;

    Rigidbody _rigidbody;

    private RocketCarMotorController carMotor;
    private RocketCarBoostController carBoost;
    private RocketCarJumpController carJump;
    private RocketCarRotationController carRotation;

    EventManager events;

    public EventManager Events
    {
        get
        {
            return events;
        }
    }

    public bool IsBlueTeam
    {
        get
        {
            return isBlueTeam;
        }

        set
        {
            isBlueTeam = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }

    public int CarVisualsIndex
    {
        get
        {
            return carVisualsIndex;
        }

        set
        {
            carVisualsIndex = value;
        }
    }

    public float CurrentBoostTime
    {
        get
        {
            return currentBoostTime;
        }
    }

    public float MaxBoostTime
    {
        get
        {
            return maxBoostTime;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        // get the car controller
        carMotor = GetComponent<RocketCarMotorController>();
        carBoost = GetComponent<RocketCarBoostController>();
        carJump = GetComponent<RocketCarJumpController>();
        carRotation = GetComponent<RocketCarRotationController>();

        events = new EventManager();

        currentBoostTime = startingBoostTime;

    }

    private void Start()
    {
        // We change the color of the car depending wether it is 
        // in the blue team
        // or not.
        carBody = Instantiate(carBodyPrefabs[carVisualsIndex],
            transform.position, transform.rotation, transform);
        MeshRenderer _rend = carBody.transform.GetChild(0).GetComponent<MeshRenderer>();
        if (isBlueTeam)
        {
            _rend.material.color = Color.blue;
        }
        else
        {
            _rend.material.color = Color.red;
        }

        gameManager = GameManager.Instance;

        if(gameManager)
        {
            GameManager.Instance.Events.RegisterCallback("OnGameStarted", OnGameStarted);
            GameManager.Instance.Events.RegisterCallback("OnGameResetted", OnGameResetted);
            GameManager.Instance.Events.RegisterCallback("OnGameOver", OnGameResetted);
        }
    }

    void OnGameStarted()
    {
        _rigidbody.isKinematic = false;
        canMove = true;
    }

    void OnGameResetted()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        carMotor.Move(0, 0, 0, 0);
        canMove = false;

        currentBoostTime = startingBoostTime;
    }

    private void Update()
    {
        isGrounded = CheckIfGrounded();
    }

    public void TreatInput(float horizontalAxis, float verticalAxis,
        float handbrake,
        bool jumpButton, bool boostButton)
    {
        if (!canMove)
        {
            return;
        }

        if(verticalAxis > 0 && !isAccelerating)
        {
            isAccelerating = true;
            events.TriggerCallback("OnAccelerationBegin");
        }

        if(verticalAxis <= 0 && isAccelerating)
        {
            isAccelerating = false;
            events.TriggerCallback("OnAccelerationEnd");
        }

        if (isGrounded)
        {
            carMotor.Move(
                horizontalAxis, verticalAxis, verticalAxis, handbrake);
        }
        else
        {
            carRotation.ApplyRotation(horizontalAxis, verticalAxis);
        }

        if (jumpButton)
        {
            if (isGrounded || canDoSecondJump)
            {
                // Basically here we test if
                // the car is grounded or can do the second
                // jump
                if (isGrounded)
                {
                    canDoSecondJump = true;
                }
                else
                {
                    canDoSecondJump = false;
                }
                carJump.ApplyJumpForce();
                events.TriggerCallback("OnJump");
            }
        }

        if (boostButton)
        {
            if (currentBoostTime > 0)
            {
                // Trigger the "begin boosting" event
                if (!isBoosting)
                {
                    isBoosting = true;
                    events.TriggerCallback("OnBoostBegin");
                }
                carBoost.Boost();
                currentBoostTime -= Time.deltaTime;
            }
            else if (isBoosting)
            {
                isBoosting = false;
                events.TriggerCallback("OnBoostEnd");
            }
        }
        else if (isBoosting)
        {
            isBoosting = false;
            events.TriggerCallback("OnBoostEnd");
        }

    }

    // We add a value ammount of boost time to our car
    public void AddBoost(float value)
    {
        currentBoostTime += value;
        // We make sure our boost time is always at most maxBoostTime
        currentBoostTime = Mathf.Clamp(
            currentBoostTime, 0, maxBoostTime);
    }

    bool CheckIfGrounded()
    {
        Vector3 downVec = (transform.up * -1).normalized;
        RaycastHit hit;

        //Debug.DrawLine(transform.position,
        //    transform.position + downVec * closeEnoughForGroundDetection,
        //    Color.green, 2);

        bool ret = false;
        if (Physics.Raycast(transform.position, downVec, out hit, closeEnoughForGroundDetection))
        {
            if (hit.transform.tag == floorTag)
            {
                ret = true;
            }
        }

        if (isGrounded && !ret)
        {
            events.TriggerCallback("OnUnGrounded");
        }
        else if (!isGrounded && ret)
        {
            events.TriggerCallback("OnGrounded");
        }

        return ret;
    }
}