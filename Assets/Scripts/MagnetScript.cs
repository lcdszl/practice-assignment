using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour {
    public PlayerMovement GamePlayer;
	// Use this for initialization
	void Start () {
        GamePlayer = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Destroy(gameObject);
            //collision.gameObject.SendMessage("SpeedUp");
            GamePlayer.velocity = GamePlayer.velocity * 2;
        }
    }
}
