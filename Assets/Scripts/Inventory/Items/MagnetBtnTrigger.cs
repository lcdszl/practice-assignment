using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBtnTrigger : MonoBehaviour {
    private Transform GamePlayer;
    
    // Use this for initialization
    private void Start () {
        GamePlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
    public void Use(){

        PlayerMovement playerMovement = GamePlayer.GetComponent<PlayerMovement>();
        playerMovement.velocity = playerMovement.velocity * 2;
        Destroy(gameObject);

    }
}
