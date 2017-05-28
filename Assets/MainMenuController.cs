using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public void Start1v1Game()
    {
        LevelManager.LoadLevel(1);
    }

    public void PickACar()
    {
        LevelManager.LoadLevel(2);
    }
}
