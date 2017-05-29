using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoredText : MonoBehaviour {
    Text _text;
	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
        _text.text = "";

        GameManager.Instance.Events.RegisterCallback("OnBlueScored", OnBlueScored);
        GameManager.Instance.Events.RegisterCallback("OnRedScored", OnRedScored);
        GameManager.Instance.Events.RegisterCallback("OnGameResetted", OnGameResetted);
    }

    void OnBlueScored()
    {
        _text.text = "blue team scored";
        _text.color = Color.blue;
    }

    void OnRedScored()
    {
        _text.text = "red team scored";
        _text.color = Color.red;
    }

    void OnGameResetted()
    {
        _text.text = "";
    }
}
