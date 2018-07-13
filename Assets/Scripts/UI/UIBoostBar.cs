using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBoostBar : MonoBehaviour {

    RocketCarManager rocketCarManager;

    float originalWidth;

    [SerializeField]
    RectTransform barRectTransform;

    void Start () {
        rocketCarManager = GameManager.Instance.PlayerGameObject.GetComponent<RocketCarManager>();
        originalWidth = barRectTransform.rect.width;
    }
	
	void Update () {
        float fillAmmount
            = rocketCarManager.CurrentBoostTime / rocketCarManager.MaxBoostTime;
        //Debug.Log("C: " + rocketCarManager.CurrentBoostTime + " M: " +
        //    rocketCarManager.MaxBoostTime);
        barRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillAmmount * originalWidth);
    }
}
