using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    [Header("Game configuration")]
    [SerializeField]
    float maxPlaytime = 300f;

    [SerializeField]
    bool playerIsBlue;
    [SerializeField]
    bool spawnEnemies = true;
    [SerializeField]
    bool spawnPlayer = true;

    [Header("Car spawns")]
    [SerializeField]
    Transform[] blueCarSpawns;
    [SerializeField]
    Transform[] redCarSpawns;

    [Header("Goal controllers")]
    [SerializeField]
    GoalController blueGoalController;
    [SerializeField]
    GoalController redGoalController;

    [SerializeField]
    Transform ballSpawn;


    [Header("Prefab references")]
    [SerializeField]
    GameObject carPrefab;
    [SerializeField]
    GameObject ballPrefab;


    float currentPlaytime = 0;
    int blueScore = 0;
    int redScore = 0;

    GameObject ballGameObject;

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

    public GameObject BallGameObject
    {
        get
        {
            return ballGameObject;
        }
    }

    public Vector3 GetGoalPos(bool isBlue)
    {
        if (isBlue)
        {
            if(blueGoalController)
                return blueGoalController.transform.position;
            return Vector3.zero;
        }
        else
        {
            if(redGoalController)
                return redGoalController.transform.position;
            return Vector3.zero;
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
        SpawnBall();
    }

    private void Start()
    {
        ChangeState(GameState.Start);
        if (blueGoalController)
        {
            blueGoalController.Events.
                RegisterCallback("OnScored", OnBlueScored);
        }
        else
        {
            Debug.LogWarning("No blue goal detected");
        }
        if (redGoalController)
        {
            redGoalController.Events.
                RegisterCallback("OnScored", OnRedScored);
        }
        else
        {
            Debug.LogWarning("No red goal detected");
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

    void SpawnBall()
    {
        GameObject go =
            Instantiate(ballPrefab, ballSpawn.position, ballSpawn.rotation);
        ballGameObject = go;
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
            if (playerIsBlue && spawnPlayer && playerIndex == i)
            {
                SpawnPlayerCar(true, blueCarSpawns[i]);
            }
            else if(spawnEnemies)
            {
                SpawnAICar(true, blueCarSpawns[i]);
            }
        }

        for (int i = 0; i < redCarSpawns.Length; ++i)
        {
            if (!playerIsBlue && spawnPlayer && playerIndex == i)
            {
                SpawnPlayerCar(false, redCarSpawns[i]);
            }
            else if(spawnEnemies)
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