using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform spawnPoint;
    public GameObject instance;
    [HideInInspector]
    public int enemyNum;

    private EnemyMovement enemyMovement;
    private FieldOfView fieldOfView;

    public void Setup()
    {
        enemyMovement = instance.GetComponent<EnemyMovement>();
        fieldOfView = instance.GetComponent<FieldOfView>();
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

        instance.SetActive(false);
        instance.SetActive(true);
    }

}
