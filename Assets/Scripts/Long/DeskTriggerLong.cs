using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTriggerLong : MonoBehaviour {

    public string triggerName;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerContent>().answers.ContainsKey(triggerName))
        {
                collision.GetComponent<PlayerContent>().answers[triggerName] = true;

        }
    }
}
