using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class EnemyManager {

    public Transform spawnPoint;
    [HideInInspector]
    public GameObject instance;
    [HideInInspector]
    public int enemyNum;

    private EnemyMovement enemyMovement;
    private FieldOfView fieldOfView;

    public void Setup(Transform[] wayPoints)
    {
        enemyMovement = instance.GetComponent<EnemyMovement>();
        fieldOfView = instance.GetComponent<FieldOfView>();
        enemyMovement.waypoints = wayPoints;
    }

    public void DisableMovement()
    {
        enemyMovement.enabled = false;
        fieldOfView.enabled = false;
    }

    public void EnableMovement()
    {
        enemyMovement.enabled = true;
        fieldOfView.enabled = true;
    }

    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        fieldOfView.foundPlayer = false;
        instance.SetActive(false);
        instance.SetActive(true);
    }

    public bool FoundPlayer()
    {
        return fieldOfView.foundPlayer;
    }

}
