using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
	public static void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
