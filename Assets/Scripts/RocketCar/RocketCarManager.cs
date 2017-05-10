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
    float startingBoostTime = 30f;
    float currentBoostTime;
    bool isBoosting = false;

    [Header("Object references")]
    [SerializeField]
    GameObject carVisuals;

    // For ground detection
    float closeEnoughForGroundDetection = 1f;
    string floorTag = "Floor";
    bool grounded;

    bool canDoSecondJump = true;

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

    private void Awake()
    {
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
        MeshRenderer _rend = carVisuals.GetComponent<MeshRenderer>();
        if (isBlueTeam)
        {
            _rend.material.color = Color.blue;
        }
        else
        {
            _rend.material.color = Color.red;
        }
    }

    public void TreatInput(float horizontalAxis, float verticalAxis,
        float handbrake,
        bool jumpButton, bool boostButton)
    {

        grounded = CheckIfGrounded();

        if (grounded)
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
            if (grounded || canDoSecondJump)
            {
                // Basically here we test if
                // the car is grounded or can do the second
                // jump
                if (grounded)
                {
                    canDoSecondJump = true;
                }
                else
                {
                    canDoSecondJump = false;
                }
                carJump.ApplyJumpForce();
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

        if (grounded && !ret)
        {
            events.TriggerCallback("OnUnGrounded");
        }
        else if (!grounded && ret)
        {
            events.TriggerCallback("OnGrounded");
        }

        return ret;
    }
}