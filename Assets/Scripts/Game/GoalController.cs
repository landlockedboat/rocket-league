using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    private EventManager events;

    private void Awake()
    {
        events = new EventManager();
    }

    public EventManager Events
    {
        get
        {
            return events;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            events.TriggerCallback("OnScored");
        }
    }

}
