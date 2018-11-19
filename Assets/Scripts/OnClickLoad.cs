using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoad : MonoBehaviour {

    public bool inPause;

    public void Start()
    {
        inPause = false;
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void PauseGame()
    {
        inPause = !inPause;
        Time.timeScale = inPause ? 0 : 1;       
    }
}
