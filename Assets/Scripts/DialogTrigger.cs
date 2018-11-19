using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public Dialog dialog;

    public IEnumerator TriggerDialog()
    {
        return FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
