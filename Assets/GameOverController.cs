using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    [SerializeField]
    Text infoText;

    private void Start()
    {
        int redScore = SceneData.Instance.GetData("red_score");
        int blueScore = SceneData.Instance.GetData("blue_score");
        bool playerIsBlue = SceneData.Instance.GetData("player_blue") == 0 ? false : true;

        bool blueWon = redScore < blueScore;

        if (redScore != blueScore)
        {
            if (blueWon == playerIsBlue)
            {
                infoText.text = "Congratulations! You won!";
            }
            else
            {
                infoText.text = "Bummer! You lost!";
            }
        }
        else
        {
            infoText.text = "Yo dude, send noodles!";
        }


    }

    public void Retry()
    {
        LevelManager.LoadLevel(1);
    }

    public void GoToMainMenu()
    {
        LevelManager.LoadLevel(0);
    }

}
