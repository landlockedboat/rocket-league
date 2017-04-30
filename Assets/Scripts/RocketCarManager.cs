using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RocketCarMotorController))]
[RequireComponent(typeof(RocketCarBoostController))]
[RequireComponent(typeof(RocketCarJumpController))]

public class RocketCarManager : MonoBehaviour
{
    float closeEnoughForGroundDetection = 1f;
    string floorTag = "Floor";

    bool canDoSecondJump = true;

    private RocketCarMotorController carMotor;
    private RocketCarBoostController carBoost;
    private RocketCarJumpController carJump;

    private void Awake()
    {
        // get the car controller
        carMotor = GetComponent<RocketCarMotorController>();
        carBoost = GetComponent<RocketCarBoostController>();
        carJump = GetComponent<RocketCarJumpController>();
    }

    public void TreatInput(float horizontalAxis, float verticalAxis,
        float handbrake, 
        bool jumpButton, bool boostButton)
    {

        bool grounded = IsGrounded();

        if (grounded)
        {
            carMotor.Move(
                horizontalAxis, verticalAxis, verticalAxis, handbrake);
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

    public bool IsGrounded()
    {
        Vector3 downVec = (transform.up * -1).normalized;
        RaycastHit hit;

        //Debug.DrawLine(transform.position,
        //    transform.position + downVec * closeEnoughForGroundDetection,
        //    Color.green, 2);

        if (Physics.Raycast(transform.position, downVec, out hit, closeEnoughForGroundDetection))
        {
            if (hit.transform.tag == floorTag)
            {
                return true;
            }
        }
        return false;
    }
}