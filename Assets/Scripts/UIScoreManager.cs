using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScoreManager : MonoBehaviour {

    GameManager gameManager;
    Text blueScoreText;
    Text redScoreText;


    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
        gameManager.Events.
            RegisterCallback("OnBlueScored", OnBlueScored);
        gameManager.Events.
            RegisterCallback("OnRedScored", OnRedScored);

        // boohoo this is bad im crying
        // shut up
        blueScoreText = transform.GetChild(0).
            GetComponent<Text>();
        redScoreText = transform.GetChild(1).
            GetComponent<Text>();
    }

    void OnBlueScored()
    {
        blueScoreText.text = gameManager.BlueScore.ToString();
    }

    void OnRedScored()
    {
        redScoreText.text = gameManager.RedScore.ToString();
    }

}
