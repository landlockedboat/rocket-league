using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    Transform target;
    // The distance in the x-z plane to the target
    [SerializeField]
    float distance = 10.0f;
    // the height we want the camera to be above the target
    [SerializeField]
    float height = 5.0f;
    [SerializeField]
    float movementDamping = 3f;
    [SerializeField]
    bool followRotation = true;

    public bool FollowRotation
    {
        get
        {
            return followRotation;
        }

        set
        {
            followRotation = value;
        }
    }

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    void FixedUpdate()
    {
        if (!target)
        {
            return;
        }


        Vector3 desiredPos = target.position;

        if (followRotation)
        {
            // Adding position relative to the destination
            desiredPos +=
                target.rotation * new Vector3(0, height, -distance);
        }
        else
        {
            desiredPos +=
                new Vector3(0, height, -distance);
        }

        desiredPos = Vector3.Lerp(
            transform.position,
            desiredPos,
            movementDamping * Time.fixedDeltaTime);

        transform.position = desiredPos;

        // Always look at the target
        transform.LookAt(target);
    }
}