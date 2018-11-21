using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerLong : MonoBehaviour {

    public EnemyRoutine[] EnemyWaypoints;
    public List<GameObject> AnswerDestinations;
    public GameObject[] Seats;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public float timeMultiplier;

    public Dialog[] PlayerDialogs;
    public Dialog[] EnemyDialogs;

    public float startWait = 2f;
    public float endWait = 2f;

    public CameraControl cameraControl;

    public EnemyManager[] enemies;
    public PlayerManagerLong[] players;

    private WaitForSeconds startWaitSec;
    private WaitForSeconds endWaitSec;

    public float totalTime;

    public GameObject timerHolder;
    public Text timerDisplay;
    public GameObject levelImage;
    public Text levelText;
    public GameObject backBtnHolder;

    public bool inDialogP = true;
    public bool inDialogE = true;

    private float timeLeft;

    private float warningDist = 50f;
    private bool foundPlayer;
    private bool allPlayerWon;

    private const string LevelStartText = "Reach The Answer!";
    private const string EndSpottedText = "You Were Spotted!";
    private const string TimeRunOutText = "Out of Time!";

    
    // Use this for initialization
    void Start () {
        InitiateSeats();
        SpawnAllPlayers();
        SpawnAllEnemies();
        cameraControl = FindObjectOfType<CameraControl>();
        startWaitSec = new WaitForSeconds(startWait);
        endWaitSec = new WaitForSeconds(endWait);

        StartCoroutine(GameLoop());
	}

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(LevelStarting());
        yield return StartCoroutine(LevelPlaying());
        yield return StartCoroutine(LevelEnding());

        StartCoroutine(GameLoop());
    }

    private IEnumerator LevelStarting()
    {
        ResetAllEnemies();
        ResetAllPlayers();
        DisableAllEnemies();
        DisableAllPlayers();
        EnableLevelUI();
        yield return startWaitSec;      
    }

    private IEnumerator LevelPlaying()
    {
        DisableLevelUI();
        inDialogE = true;
        inDialogP = true;
        while (inDialogP)
        {
            yield return EnableAllPlayersDialog();
        }

        EnableTimerUI();
        EnableAllEnemies();
        EnableAllPlayers();
        while (!foundPlayer && !allPlayerWon && timeLeft>0f)
        {
            foundPlayer = FoundPlayer();
            allPlayerWon = AllPlayerWon();
            IncrementTimer();
            SetEnemyIndicator();
            yield return null;
        }
    }

    private IEnumerator LevelEnding()
    {
        DisableTimerUI();
        DisableAllEnemies();
        DisableAllPlayers();
        SetEndScreen();
        EnableLevelUI();
        yield return endWaitSec;
    }


    public void SpawnAllEnemies()
    {
        for (int i=0; i<enemies.Length; i++)
        {
            enemies[i].instance = Instantiate(enemyPrefab, enemies[i].spawnPoint.position, enemies[i].spawnPoint.rotation) as GameObject;
            enemies[i].Setup(EnemyWaypoints[i].EnemyWaypoints);
            //enemies[i].dialog.dialog = EnemyDialogs[i];
            //enemies[i].dialog.dialog.CameraLocation = enemies[i].instance.transform.position;
        }

    }

    public void SpawnAllPlayers()
    {

        for (int i=0; i<players.Length; i++)
        {
            //players[i].SetHomeSeat(Seats[UnityEngine.Random.Range(0, Seats.Length - 1)]);
            players[i].instance = Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            PlayerList.players.Add(players[i].instance);
            players[i].Setup(AnswerDestinations);
            players[i].dialog.dialog = PlayerDialogs[i];
            players[i].dialog.dialog.CameraLocation = players[i].instance.transform.position;
        }

    }

    public void ResetAllEnemies()
    {
        for (int i=0; i<enemies.Length; i++)
        {
            enemies[i].Reset();
        }
        foundPlayer = false;
    }

    public void ResetAllPlayers()
    {
        for (int i=0; i<players.Length; i++)
        {
            //players[i].SetHomeSeat(Seats[UnityEngine.Random.Range(0, Seats.Length - 1)]);
            players[i].Reset();
        }
        levelText.text = LevelStartText;
        allPlayerWon = false;
    }

    public void DisableAllEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].DisableMovement();
        }
    }

    public void DisableAllPlayers()
    {
        cameraControl.inGame = false;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }

    public void EnableAllEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].EnableMovement();
        }
    }

    public void EnableAllPlayers()
    {
        cameraControl.inGame = true;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }

    public IEnumerator EnableAllPlayersDialog()
    {
        
        for (int i = 0; i < players.Length; i++)
        {
            yield return StartDialogTurn(players[i].instance);
        }
        inDialogP = false;
    }

    public IEnumerator EnableAllEnemyDialog()
    {
        for (int i=0; i<enemies.Length; i++)
        {
            yield return StartDialogTurn(enemies[i].instance);
        }
        inDialogE = false;
    }

    public void SetEnemyIndicator()
    {
        for (int i = 0; i < players.Length; i++)
        {
            float dist = float.MaxValue;
            Vector3 playerLocation = players[i].instance.transform.position;
            Vector3 closestEnemy = new Vector3();
            for (int j = 0; j < enemies.Length; j++)
            {
                if (Vector3.Distance(enemies[j].instance.transform.position, playerLocation) <= dist)
                {
                    closestEnemy = enemies[j].instance.transform.position;
                    dist = Vector3.Distance(enemies[j].instance.transform.position, playerLocation);
                }
            }
            if (dist < warningDist)
            {
                if (Mathf.Abs((closestEnemy-playerLocation).y) < Mathf.Abs((closestEnemy - playerLocation).x))
                {
                    if (closestEnemy.x > playerLocation.x)
                    {
                        players[i].playerContent.enemyDirection = DirectionEnum.Directions.Right;
                    }
                    else
                    {
                        players[i].playerContent.enemyDirection = DirectionEnum.Directions.Left;
                    }
                }
                else
                {
                    if (closestEnemy.y > playerLocation.y)
                    {
                        players[i].playerContent.enemyDirection = DirectionEnum.Directions.Up;
                    }
                    else
                    {
                        players[i].playerContent.enemyDirection = DirectionEnum.Directions.Down;
                    }
                }
            }
            else
            {
                players[i].playerContent.enemyDirection = DirectionEnum.Directions.None;
            }
        }
    }

    private void EnableLevelUI()
    {       
        levelImage.SetActive(true);
    }

    private void DisableLevelUI()
    {
        levelImage.SetActive(false);
    }

    private void EnableTimerUI()
    {
        timeLeft = totalTime;
        timerHolder.SetActive(true);
        backBtnHolder.SetActive(true);
    }

    private void DisableTimerUI()
    {
        timerHolder.SetActive(false);
        backBtnHolder.SetActive(false);
    }

    private void IncrementTimer()
    {
        timeLeft -= Time.deltaTime;
        timerDisplay.text = Convert.ToString(Mathf.Round(timeLeft));
    }

    private bool FoundPlayer()
    {
        bool found = false; 
        for (int i=0; i<enemies.Length; i++)
        {
            found = found || enemies[i].FoundPlayer();
        }
        return found;
    }

    private bool AllPlayerWon()
    {
        bool hasWon = true;
        for (int i = 0; i < players.Length; i++)
        {         
            hasWon = hasWon && players[i].playerContent.HasWon();
        }
        return hasWon;
    }

    private void SetEndScreen()
    {
        if (foundPlayer)
        {
            levelText.text = EndSpottedText;
        }
        else if (allPlayerWon)
        {
            levelText.text = "GOOD JOB! \n\n";
            for (int i = 0; i < players.Length; i++)
            {
                levelText.text += "Player" + (i + 1) + "'s Score: " + CalcScore() + "\n\n"; 
            }
        }
        else if (timeLeft <= 0)
        {
            levelText.text = TimeRunOutText;
        }
    }

    private float CalcScore()
    {
        return Mathf.Round(timeMultiplier * timeLeft);
    }

    private void InitiateSeats()
    {
        for (int i = 0; i < Seats.Length; i++)
        {
            Seats[i].GetComponent<SeatTrigger>().seatNum = i + 1;
        }
    }

    private IEnumerator StartDialogTurn(GameObject gameObject)
    {
        DialogTrigger dTrigger = gameObject.GetComponent<DialogTrigger>();
        cameraControl.MoveCamera(gameObject.transform.position);
        return dTrigger.TriggerDialog();
    }
}
