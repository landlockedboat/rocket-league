using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCam : MonoBehaviour {

    Transform ball;
    Transform car;

    [SerializeField]
    float distance = 10.0f;
    [SerializeField]
    float height = 5f;

    public Transform Ball
    {
        get
        {
            return ball;
        }

        set
        {
            ball = value;
        }
    }

    public Transform Car
    {
        get
        {
            return car;
        }

        set
        {
            car = value;
        }
    }

    public float Distance
    {
        get
        {
            return distance;
        }

        set
        {
            distance = value;
        }
    }

    void FixedUpdate()
    {
        Vector3 carPos = car.position;
        Vector3 ballDir = Vector3.Normalize(ball.position - transform.position);
        Vector3 desiredPos = (ballDir * distance) + carPos;
        desiredPos = desiredPos + Vector3.up * height;

        transform.position = desiredPos;

        transform.LookAt(ball);
    }
}
