using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public void Start1v1Game()
    {
        SceneData.Instance.SetData("cars_per_team", 1);
        LevelManager.LoadLevel(2);
    }

    public void Start2v2Game()
    {
        SceneData.Instance.SetData("cars_per_team", 2);
        LevelManager.LoadLevel(2);
    }

    public void Start3v3Game()
    {
        SceneData.Instance.SetData("cars_per_team", 3);
        LevelManager.LoadLevel(2);
    }

    public void PickACar()
    {
        LevelManager.LoadLevel(3);
    }
}
