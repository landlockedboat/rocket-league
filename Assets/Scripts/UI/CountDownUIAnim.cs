using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDownUIAnim : MonoBehaviour {

    Text countDownText;
    GameManager gameManager;

    bool showText = false;


	void Start () {
        countDownText = GetComponent<Text>();

        gameManager = GameManager.Instance;
        gameManager.Events.RegisterCallback("OnGameStarted", OnGameStarted);
        gameManager.Events.RegisterCallback("OnGameResetted", OnGameResetted);
    }


    void OnGameResetted()
    {
        showText = true;
    }

    void OnGameStarted()
    {
        showText = false;
        countDownText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (!showText)
            return;
        countDownText.text = gameManager.CurrentStartDownTime.ToString("0");
	}
}
