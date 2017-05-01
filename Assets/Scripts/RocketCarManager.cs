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
    bool isPlayer = false;

    float closeEnoughForGroundDetection = 1f;
    string floorTag = "Floor";
    bool grounded;

    bool canDoSecondJump = true;

    private RocketCarMotorController carMotor;
    private RocketCarBoostController carBoost;
    private RocketCarJumpController carJump;
    private RocketCarRotationController carRotation;

    public bool Grounded
    {
        get
        {
            return grounded;
        }

    }

    private void Awake()
    {
        // get the car controller
        carMotor = GetComponent<RocketCarMotorController>();
        carBoost = GetComponent<RocketCarBoostController>();
        carJump = GetComponent<RocketCarJumpController>();
        carRotation = GetComponent<RocketCarRotationController>();
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

        if(jumpButton)
        {
            if (grounded || canDoSecondJump)
            {
                // Basically here we test if
                // the car is grounded or can do the second
                // jump
                if(grounded)
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

        if(boostButton)
        {
            carBoost.Boost();
        }

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

        if (isPlayer)
        {
            if(grounded && !ret)
            {
                EventManager.TriggerCallback("OnPlayerUnGrounded");
            }
            else if(!grounded && ret)
            {
                EventManager.TriggerCallback("OnPlayerGrounded");
            }
        }

        return ret;
    }
}