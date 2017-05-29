using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

	public void Retry()
    {
        LevelManager.LoadLevel(1);
    }

    public void GoToMainMenu()
    {
        LevelManager.LoadLevel(0);
    }

}
