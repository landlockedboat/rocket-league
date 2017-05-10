using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    [SerializeField]
    bool playerIsBlue;
    [SerializeField]
    bool spawnEnemies;

    [Header("Car spawns")]
    [SerializeField]
    Transform[] blueCarSpawns;
    [SerializeField]
    Transform[] redCarSpawns;

    [Header("Object references")]
    [SerializeField]
    GameObject AIcarPrefab;
    [SerializeField]
    GameObject playerCarPrefab;

    void SpawnAICar(bool isBlue, Transform pivot)
    {
        GameObject go = 
            Instantiate(AIcarPrefab, pivot.position, pivot.rotation);
        go.GetComponent<RocketCarManager>().IsBlueTeam = isBlue;
    }

    void SpawnPlayerCar(bool isBlue, Transform pivot)
    {
        GameObject go =
            Instantiate(playerCarPrefab, pivot.position, pivot.rotation);
        go.GetComponent<RocketCarManager>().IsBlueTeam = isBlue;
    }

    void Start () {

        int playerIndex = 0;
        if (playerIsBlue)
        {
            playerIndex = Random.Range(0, blueCarSpawns.Length);
        }
        else
        {
            playerIndex = Random.Range(0, redCarSpawns.Length);
        }

		for(int i = 0; i < blueCarSpawns.Length; ++i)
        {
            if(playerIsBlue && playerIndex == i)
            {
                SpawnPlayerCar(true, blueCarSpawns[i]);
            }
            else
            {
                SpawnAICar(true, blueCarSpawns[i]);
            }
        }

        for (int i = 0; i < redCarSpawns.Length; ++i)
        {
            if (!playerIsBlue && playerIndex == i)
            {
                SpawnPlayerCar(true, redCarSpawns[i]);
            }
            else
            {
                SpawnAICar(true, redCarSpawns[i]);
            }
        }
    }
	
	void Update () {
		
	}
}
