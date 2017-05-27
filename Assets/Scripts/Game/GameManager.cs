using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    [Header("Game configuration")]
    float maxPlaytime = 300f;

    [SerializeField]
    bool playerIsBlue;
    [SerializeField]
    bool spawnEnemies;

    [Header("Car spawns")]
    [SerializeField]
    Transform[] blueCarSpawns;
    [SerializeField]
    Transform[] redCarSpawns;

    [SerializeField]
    GoalController blueGoalController;
    [SerializeField]
    GoalController redGoalController;


    [Header("Object references")]
    [SerializeField]
    GameObject carPrefab;

    float currentPlaytime = 0;
    int blueScore = 0;
    int redScore = 0;

    static GameManager instance;
    GameState currentState;

    private EventManager events;


    public static GameManager Instance
    {
        get { return instance; }
    }

    public GameState CurrentState
    {
        get { return currentState; }
        set { ChangeState(value); }
    }

    public EventManager Events
    {
        get
        {
            return events;
        }
    }

    public int BlueScore
    {
        get
        {
            return blueScore;
        }
    }

    public int RedScore
    {
        get
        {
            return redScore;
        }
    }

    public float CurrentPlaytime
    {
        get
        {
            return currentPlaytime;
        }
    }

    void ChangeState(GameState state)
    {
        currentState = state;
    }

    void Awake()
    {
        instance = FindObjectOfType<GameManager>();
        events = new EventManager();
        currentPlaytime = maxPlaytime;
        SpawnCars();
    }

    private void Start()
    {
        ChangeState(GameState.Start);
        if (blueGoalController)
        {
            blueGoalController.Events.
                RegisterCallback("OnScored", OnBlueScored);
        }
        if (redGoalController)
        {
            redGoalController.Events.
                RegisterCallback("OnScored", OnRedScored);
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Start:
                ChangeState(GameState.Countdown);
                break;
            case GameState.Countdown:
                ChangeState(GameState.Playing);
                break;
            case GameState.Playing:
                currentPlaytime -= Time.deltaTime;
                if(currentPlaytime <= 0)
                {
                    Debug.Log("No time, buddy");
                    ChangeState(GameState.GameOver);
                }
                break;
            case GameState.Scored:
                break;
            default:
                break;
        }
    }

    void OnBlueScored()
    {
        // Debug.Log("Blue player scored!");
        ++blueScore;
        events.TriggerCallback("OnBlueScored");
    }

    void OnRedScored()
    {
        // Debug.Log("Red player scored!");
        ++redScore;
        events.TriggerCallback("OnRedScored");
    }

    void SpawnAICar(bool isBlue, Transform pivot)
    {
        GameObject go =
            Instantiate(carPrefab, pivot.position, pivot.rotation);
        go.GetComponent<RocketCarManager>().IsBlueTeam = isBlue;
        go.AddComponent<RocketCarAIControl>();
    }

    void SpawnPlayerCar(bool isBlue, Transform pivot)
    {
        GameObject go =
            Instantiate(carPrefab, pivot.position, pivot.rotation);
        go.GetComponent<RocketCarManager>().IsBlueTeam = isBlue;
        go.AddComponent<RocketCarPlayerControl>();
    }

    void SpawnCars()
    {
        int playerIndex = 0;
        if (playerIsBlue)
        {
            playerIndex = Random.Range(0, blueCarSpawns.Length);
        }
        else
        {
            playerIndex = Random.Range(0, redCarSpawns.Length);
        }

        for (int i = 0; i < blueCarSpawns.Length; ++i)
        {
            if (playerIsBlue && playerIndex == i)
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
                SpawnPlayerCar(false, redCarSpawns[i]);
            }
            else
            {
                SpawnAICar(false, redCarSpawns[i]);
            }
        }
    }
}

public enum GameState
{
    // A fictional state just before the countdown of the game
    Start,
    // The countdown before hitting the ball
    Countdown,
    // Generic playing state
    Playing,
    // When someone scores a goal
    Scored,
    // When time reaches 0
    GameOver,
}