using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public EnemyRoutine[] EnemyWaypoints;
    public List<GameObject> AnswerDestinations;
    public GameObject[] Seats;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public float timeMultiplier;

    public float startWait = 2f;
    public float endWait = 2f;

    public EnemyManager[] enemies;
    public PlayerManager[] players;

    private WaitForSeconds startWaitSec;
    private WaitForSeconds endWaitSec;

    public float totalTime;

    public GameObject timerHolder;
    public Text timerDisplay;
    public GameObject levelImage;
    public Text levelText;

    private float timeLeft;

    private bool foundPlayer;
    private bool allPlayerWon;
    // Use this for initialization
    void Start () {
        SpawnAllPlayers();
        SpawnAllEnemies();

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
        EnableTimerUI();
        EnableAllEnemies();
        EnableAllPlayers();
        while (!foundPlayer && !allPlayerWon)
        {
            foundPlayer = FoundPlayer();
            allPlayerWon = AllPlayerWon();
            IncrementTimer();
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
        }

    }

    public void SpawnAllPlayers()
    {

        for (int i=0; i<players.Length; i++)
        {
            players[i].homeSeat = Seats[UnityEngine.Random.Range(0, Seats.Length - 1)];
            players[i].instance = Instantiate(playerPrefab, players[i].homeSeat.transform.position + Positions.seatOffset, players[i].homeSeat.transform.rotation) as GameObject;
            players[i].Setup(AnswerDestinations);
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
            players[i].homeSeat = Seats[UnityEngine.Random.Range(0, Seats.Length - 1)];
            players[i].Reset();
        }
        levelText.text = "Get the answer!";
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
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
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
    }

    private void DisableTimerUI()
    {
        timerHolder.SetActive(false);
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
            levelText.text = "YOU WERE SPOTTED!";
        }
        else if (allPlayerWon)
        {
            levelText.text = "GOOD JOB! \n\n";
            for (int i = 0; i < players.Length; i++)
            {
                levelText.text += "Player" + (i + 1) + "'s Score: " + CalcScore() + "\n\n"; 
            }
        }
    }

    private float CalcScore()
    {
        return Mathf.Round(timeMultiplier * timeLeft);
    }
}
