using UnityEngine;

public class Matha
{
    public static float Angle(Vector3 source, Vector3 destination)
    {
        Vector3 dir = destination - source;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        return angle;
    }
}
