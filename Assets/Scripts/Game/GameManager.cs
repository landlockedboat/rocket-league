using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    [Header("Game configuration")]
    [SerializeField]
    float maxPlaytime = 300f;
    [SerializeField]
    float startDownTime = 3f;
    float currentStartDownTime;
    [SerializeField]
    float scoredDownTime = 3f;
    float currentScoredDownTime;
    [SerializeField]
    float gameOverDownTime = 3f;
    float currentGameOverDownTime;

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

    int carsPerTeam;

    GameObject ballGameObject;

    List<GameObject> blueCars = new List<GameObject>();
    List<GameObject> redCars = new List<GameObject>();

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

    public float CurrentStartDownTime
    {
        get
        {
            return currentStartDownTime;
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
        switch (state)
        {
            case GameState.Start:
                currentStartDownTime = startDownTime;
                ResetPositions();
                Debug.Log("OnGameResetted");
                events.TriggerCallback("OnGameResetted");
                break;
            case GameState.Playing:
                Debug.Log("OnGameStarted");
                events.TriggerCallback("OnGameStarted");
                break;
            case GameState.Scored:
                currentScoredDownTime = scoredDownTime;
                break;
            case GameState.GameOver:
                Debug.Log("OnGameOver");
                events.TriggerCallback("OnGameOver");
                currentGameOverDownTime = gameOverDownTime;
                break;
            default:
                break;
        }
        currentState = state;
    }

    void ResetPositions()
    {
        for (int i = 0; i < carsPerTeam; ++i)
        {
            blueCars[i].transform.position = blueCarSpawns[i].position;
            blueCars[i].transform.rotation = blueCarSpawns[i].rotation;
        }

        for (int i = 0; i < carsPerTeam; ++i)
        {
            redCars[i].transform.position = redCarSpawns[i].position;
            redCars[i].transform.rotation = redCarSpawns[i].rotation;
        }

        ballGameObject.transform.position = ballSpawn.position;
    }

    void Awake()
    {
        instance = FindObjectOfType<GameManager>();
        events = new EventManager();
        currentPlaytime = maxPlaytime;

        carsPerTeam = SceneData.Instance.GetData("cars_per_team");
        if (carsPerTeam < 0)
        {
            carsPerTeam = 3;
        }

        SpawnCars();
        SpawnBall();
    }

    private void Start()
    {
        ChangeState(GameState.Start);
        if (blueGoalController)
        {
            blueGoalController.Events.
                RegisterCallback("OnScored", OnRedScored);
        }
        else
        {
            Debug.LogWarning("No blue goal detected");
        }
        if (redGoalController)
        {
            redGoalController.Events.
                RegisterCallback("OnScored", OnBlueScored);
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
                currentStartDownTime -= Time.deltaTime;
                if(currentStartDownTime <= 0)
                {
                    ChangeState(GameState.Playing);                    
                }
                break;
            case GameState.Playing:
                currentPlaytime -= Time.deltaTime;
                if(currentPlaytime <= 0)
                {
                    ChangeState(GameState.GameOver);
                }
                break;
            case GameState.Scored:
                currentScoredDownTime -= Time.deltaTime;
                if (currentScoredDownTime <= 0)
                {
                    ChangeState(GameState.Start);                    
                }
                break;
            case GameState.GameOver:
                currentGameOverDownTime -= Time.deltaTime;
                if (currentGameOverDownTime <= 0)
                {
                    LevelManager.LoadLevel(3);
                }
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
        ChangeState(GameState.Scored);
    }

    void OnRedScored()
    {
        // Debug.Log("Red player scored!");
        ++redScore;
        events.TriggerCallback("OnRedScored");
        ChangeState(GameState.Scored);
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
        RocketCarManager rcm = go.GetComponent<RocketCarManager>();
        rcm.IsBlueTeam = isBlue;
        rcm.CarVisualsIndex = Random.Range(0,
            SceneData.Instance.GetData("car_models"));
        go.AddComponent<RocketCarAIControl>();

        if (isBlue)
        {
            blueCars.Add(go);
        }
        else
        {
            redCars.Add(go);
        }
    }

    void SpawnPlayerCar(bool isBlue, Transform pivot)
    {
        GameObject go =
            Instantiate(carPrefab, pivot.position, pivot.rotation);
        RocketCarManager rcm = go.GetComponent<RocketCarManager>();
        rcm.IsBlueTeam = isBlue;
        rcm.CarVisualsIndex = SceneData.Instance.GetData("picked_car");
        go.AddComponent<RocketCarPlayerControl>();
        if (isBlue)
        {
            blueCars.Add(go);
        }
        else
        {
            redCars.Add(go);
        }
    }

    void SpawnCars()
    {
        //int playerIndex = Random.Range(0, carsPerTeam);
        //TODO FIX
        int playerIndex = 0;

        for (int i = 0; i < carsPerTeam; ++i)
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

        for (int i = 0; i < carsPerTeam; ++i)
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
    Start,
    Playing,
    Scored,
    GameOver,
}