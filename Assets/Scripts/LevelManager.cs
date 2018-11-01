using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public EnemyRoutine[] EnemyWaypoints;
    public List<GameObject> AnswerDestinations;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

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
        while (!FoundPlayer())
        {
            IncrementTimer();
            yield return null;
        }
    }

    private IEnumerator LevelEnding()
    {
        DisableTimerUI();
        DisableAllEnemies();
        DisableAllPlayers();
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
            players[i].instance = Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].Setup(AnswerDestinations);
        }

    }

    public void ResetAllEnemies()
    {
        for (int i=0; i<enemies.Length; i++)
        {
            enemies[i].Reset();
        }
    }

    public void ResetAllPlayers()
    {
        for (int i=0; i<players.Length; i++)
        {
            players[i].Reset();
        }
        levelText.text = "Get the answer!";
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
        levelText.text = found ? "You have been seen!" : "Good Job!";
        return found;
    }

}
