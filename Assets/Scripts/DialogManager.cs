using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    private Queue<string> sentences;

    public DialogManager()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialogToDisplay)
    {
        sentences.Clear();
        Debug.Log("start dialog");
        foreach (string sentence in dialogToDisplay.Dialogs)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextDialog();
    }

    public void DisplayNextDialog()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        Debug.Log(sentences.Dequeue());
    }

    public void EndDialog()
    {
        Debug.Log("end dialog");
    }

}
