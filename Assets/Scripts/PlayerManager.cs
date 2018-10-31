using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerManager {
    [HideInInspector]
    public Color playerColor;
    [HideInInspector]
    public int playerNum;
    [HideInInspector]
    public GameObject instance;

    public Transform spawnPoint;

    private PlayerMovement playerMovement;
    private PlayerContent playerContent;

    public void Setup(List<GameObject> answerDests)
    {
        playerMovement = instance.GetComponent<PlayerMovement>();
        playerContent = instance.GetComponent<PlayerContent>();
        playerContent.answerDestinations = answerDests;
        playerContent.SetAnswers();
    }

    public void DisableControl()
    {
        playerMovement.enabled = false;
        playerContent.enabled = false;
    }

    public void EnableControl()
    {
        playerMovement.enabled = true;
        playerContent.enabled = true;
    }

    public void Reset()
    {
        instance.transform.position = spawnPoint.position;

        instance.SetActive(false);
        instance.SetActive(true);
    }

}
