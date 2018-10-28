using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    [HideInInspector]
    public Color playerColor;
    [HideInInspector]
    public int playerNum;
    [HideInInspector]
    public GameObject instance;

    public Transform spawnPoint;

    private PlayerMovement playerMovement;
    private PlayerContent playerContent;

    public void Setup()
    {
        playerMovement = instance.GetComponent<PlayerMovement>();
        playerContent = instance.GetComponent<PlayerContent>();
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
