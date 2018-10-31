using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return startWaitSec;
    }

    private IEnumerator LevelPlaying()
    {
        EnableAllEnemies();
        EnableAllPlayers();
        while (!FoundPlayer())
        {
            yield return null;
        }
    }

    private IEnumerator LevelEnding()
    {
        DisableAllEnemies();
        DisableAllPlayers();
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

    private bool FoundPlayer()
    {
        bool found = false; 
        for (int i=0; i<enemies.Length; i++)
        {
            found = found || enemies[i].FoundPlayer();
        }
        return found;
    }
}
