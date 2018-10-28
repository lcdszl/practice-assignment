using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerContent>().ReachedSeat();
    }

}
