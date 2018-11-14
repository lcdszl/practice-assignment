using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;

    public Animator animator;

    private Queue<Dialog> dialogs;
    private Queue<string> sentences;
    private string NEXTBUTTON = "Submit";

    public DialogManager()
    {
        dialogs = new Queue<Dialog>();
        sentences = new Queue<string>();
    }

    public void ClearDialogs()
    {
        dialogs.Clear();
    }

    public void PushDialog(Dialog dialogToDisplay)
    {
        //FindObjectOfType<LevelManager>().inDialog = true;
        Debug.Log("push dialog");
        //animator.SetBool("IsOpen", true);
        dialogs.Enqueue(dialogToDisplay);
    }

    public IEnumerator StartDialog(Dialog dialogToDisplay)
    {
        //FindObjectOfType<LevelManager>().inDialog = true;
        sentences.Clear();
        Debug.Log("start dialog");
        animator.SetBool("IsOpen", true);
        //Dialog dialogToDisplay = dialogs.Dequeue();
        foreach (string sentence in dialogToDisplay.Dialogs)
        {
            sentences.Enqueue(sentence);
        }
        nameText.text = dialogToDisplay.Name;
        return DisplayAllSentence();
    }

    public IEnumerator DisplayAllSentence()
    {
        if (sentences.Count > 0)
        {
            dialogText.text = sentences.Dequeue();
        }
        while (sentences.Count >= 0)
        {
#if UNITY_STANDALONE || UNITY_STANDALONE_OSX
            if (Input.GetButtonDown(NEXTBUTTON))
#elif UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
#endif
                {
                    if (sentences.Count == 0)
                    {
                        EndDialog();
                        break;
                    }
                    else
                    {
                        dialogText.text = sentences.Dequeue();
                    }
                }
#if UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
            }
#endif
            yield return null;
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        dialogText.text = sentences.Dequeue();
    }

    public void EndDialog()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("end dialog");
        //FindObjectOfType<LevelManager>().inDialog = false;
    }

}
