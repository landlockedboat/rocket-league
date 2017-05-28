using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    [SerializeField]
    float updateDistanceTime = .2f;

    Vector3 blueGoal;
    Vector3 redGoal;

    float distanceToRedGoal = float.PositiveInfinity;
    float distanceToBlueGoal = float.PositiveInfinity;

    void Start () {
        blueGoal = GameManager.Instance.GetGoalPos(true);
        redGoal = GameManager.Instance.GetGoalPos(false);
        InvokeRepeating("UpdateDistanceToGoals", 0, updateDistanceTime);
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
