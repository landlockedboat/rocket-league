using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    AudioSource musicSource;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        musicSource = GetComponent<AudioSource>();
    }

    bool musicActive = true;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            musicActive = !musicActive;
            if(musicActive)
            {
                musicSource.Play();
            }
            else
            {
                musicSource.Pause();
            }
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            bool soundActive = SceneData.Instance.
                GetData("sound") == 0 ? false: true;
            soundActive = !soundActive;

            SceneData.Instance.SetData("sound", soundActive ? 1 : 0);
        }
	}
}
