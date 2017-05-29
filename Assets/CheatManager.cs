using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.CurrentPlaytime = 10;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.LoadLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.LoadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.AddBlueScore();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.Instance.AddRedScore();
        }
    }
}
