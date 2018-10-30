using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Transform[] EnemiesLocations;
    public Transform[] PlayerLocations;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public float startWait = 2f;
    public float endWait = 2f;

    public EnemyManager[] enemies;
    public PlayerManager[] players;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        yield return startWait;
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
        yield return endWait;
    }


    public void SpawnAllEnemies()
    {
        if (EnemiesLocations.Length != enemies.Length)
        {
            return;
        }
        else
        {
            for (int i=0; i<EnemiesLocations.Length; i++)
            {
                enemies[i].instance = Instantiate(enemyPrefab, EnemiesLocations[i]);
                enemies[i].Setup();
            }
        }
    }

    public void SpawnAllPlayers()
    {
        if (PlayerLocations.Length != players.Length)
        {
            return;
        }
        else
        {
            for (int i=0; i<PlayerLocations.Length; i++)
            {
                players[i].instance = Instantiate(playerPrefab, PlayerLocations[i]);
                players[i].Setup();
            }
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
