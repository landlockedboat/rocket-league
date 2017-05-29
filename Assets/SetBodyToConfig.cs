using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBodyToConfig : MonoBehaviour {

	void Awake () {
        gameObject.GetComponent<RocketCarManager>().CarVisualsIndex =
            SceneData.Instance.GetData("picked_car");
        gameObject.GetComponent<RocketCarManager>().IsBlueTeam =
            SceneData.Instance.GetData("is_blue") == 0 ? false : true;

    }
}
