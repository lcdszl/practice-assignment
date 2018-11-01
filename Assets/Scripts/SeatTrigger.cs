using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTrigger : MonoBehaviour {

    public int seatNum;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //collision.GetComponent<PlayerContent>().ReachedSeat();
        collision.GetComponent<PlayerContent>().currentSeatNum = seatNum;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //collision.GetComponent<PlayerContent>().LeftSeat();
        collision.GetComponent<PlayerContent>().currentSeatNum = 0;
    }

}
