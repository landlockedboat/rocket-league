using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RocketCarManager))]
public class RocketCarAIControl : MonoBehaviour
{

    [SerializeField]
    float accelWhenRotating = .3f;

    [SerializeField]
    float minSteeringAngle = 5;
    [SerializeField]
    float maxSteeringAngle = 45;

    [SerializeField]
    float behindTheBallPivotDistance = 10;
    [SerializeField]
    float distanceBehindBallHisteresy = 10;

    // With how much frequency we have to check if we are
    // behind the ball or not
    [SerializeField]
    float checkTime = .2f;

    private RocketCarManager carManager;

    Transform ballTransform;
    BallManager ballManager;
    Vector3 enemyGoalPos;
    Vector3 friendlyGoalPos;
    bool behindTheBall = true;

    private void Awake()
    {
        // get the car controller
        carManager = GetComponent<RocketCarManager>();
    }

    private void Start()
    {
        ballTransform = GameManager.Instance.
            BallGameObject.transform;
        ballManager = ballTransform.gameObject.GetComponent<BallManager>();

        enemyGoalPos = GameManager.Instance.GetGoalPos(!carManager.IsBlueTeam);
        friendlyGoalPos = GameManager.Instance.GetGoalPos(carManager.IsBlueTeam);

        InvokeRepeating("CheckIfBehidTheBall", 0, checkTime);

    }

    private void CheckIfBehidTheBall()
    {
        float distToEnemyGoal =
            Vector3.Distance(transform.position, enemyGoalPos);
        float ballDistToEnemyGoal = ballManager.GetDistanceToGoal(
            !carManager.IsBlueTeam);

        if (behindTheBall)
        {
            if (distToEnemyGoal < ballDistToEnemyGoal - distanceBehindBallHisteresy)
            {
                //Debug.Log("In front of the ball");
                behindTheBall = false;
            }
        }
        else
        {
            if (distToEnemyGoal > ballDistToEnemyGoal + distanceBehindBallHisteresy)
            {
                //Debug.Log("Behind the ball");
                behindTheBall = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 objective = Vector3.zero;

        if (behindTheBall)
        {
            objective = ballTransform.position;
        }
        else
        {
            Vector3 fromGoalDir = Vector3.Normalize(
                ballTransform.position - enemyGoalPos);

            objective = ballTransform.position + fromGoalDir *
                behindTheBallPivotDistance;
        }

        float ballAngle = Matha.Angle(transform.position, objective);
        float currentRotation = transform.eulerAngles.y - 90;
        float desiredRotation = ballAngle + currentRotation;

        //Debug.Log("A: " + ballAngle +
        //" C: " + currentRotation + " D: " + desiredRotation);

        float steering = 0;
        float accel = 1;
        if (desiredRotation < 0)
        {
            desiredRotation += 360;
        }

        if (desiredRotation > minSteeringAngle)
        {
            steering = desiredRotation / maxSteeringAngle;
            steering = Mathf.Clamp01(steering);

            accel = accelWhenRotating;

            if (desiredRotation > 180)
            {
                //Debug.Log("Ball is right");
            }
            else
            {
                //Debug.Log("Ball is left");
                steering *= -1;
            }
        }

        if (carManager.IsGrounded)
        {
            carManager.TreatInput(steering, .5f, 0, false, false);
        }
    }

}
