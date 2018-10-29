using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour {

    public string triggerName;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerContent>().answers.ContainsKey(triggerName))
        {
            if (collision.GetComponent<PlayerContent>().fetchProgress < 100.0f)
            {
                collision.GetComponent<PlayerContent>().fetchProgress += collision.GetComponent<PlayerContent>().fetchSpeed * Time.deltaTime;
            }
            else
            {
                collision.GetComponent<PlayerContent>().answers[triggerName] = true;
            }
        }
    }
}
