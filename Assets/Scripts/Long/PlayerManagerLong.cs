using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerManagerLong
{
    public Transform spawnPoint;
    [HideInInspector]
    public Color playerColor;
    [HideInInspector]
    public int playerNum;
    [HideInInspector]
    public GameObject instance;

    public GameObject homeSeat;
    [HideInInspector]
    public PlayerMovementLong playerMovement;
    [HideInInspector]
    public PlayerContentLong playerContent;
    [HideInInspector]
    public DialogTrigger dialog;
    private const int SPAWN_INDEX = 1;

    public virtual void Setup(List<GameObject> answerDests)
    {
        playerMovement = instance.GetComponent<PlayerMovementLong>();
        playerContent = instance.GetComponent<PlayerContentLong>();
        dialog = instance.GetComponent<DialogTrigger>();
        playerContent.answerDestinations = answerDests;
        playerContent.homeSeat = homeSeat;
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

    public void SetHomeSeat(GameObject homeSeat)
    {
        this.homeSeat = homeSeat;
    }

    public void Reset()
    {
        instance.transform.position = spawnPoint.transform.position;
        //playerContent.homeSeat = homeSeat;
        instance.SetActive(false);
        instance.SetActive(true);
    }

}
