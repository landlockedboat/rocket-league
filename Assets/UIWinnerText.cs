using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWinnerText : MonoBehaviour {

    GameManager gameManager;
    Text _text;

    private void Start()
    {
        gameManager = GameManager.Instance;
        _text = GetComponent<Text>();
        _text.text = "";
        gameManager.Events.RegisterCallback("OnGameOver", OnGameOver);
    }

    void OnGameOver()
    {
        if(gameManager.BlueScore > gameManager.RedScore)
        {
            _text.text = "BLUE TEAM WON!";
            _text.color = Color.blue;
        }
        else if(gameManager.BlueScore < gameManager.RedScore)
        {
            _text.text = "RED TEAM WON!";
            _text.color = Color.red;
        }
        else
        {
            _text.text = "ABER ESTUDIAO!";
            _text.color = Color.yellow;
        }
    }

}
