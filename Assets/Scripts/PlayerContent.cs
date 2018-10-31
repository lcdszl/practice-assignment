using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour {

    public float fetchSpeed = 30.0f;

    [Range(0.0f, 100.0f)]
    public float fetchProgress;

    public int playerNum;
    public Slider fetchSlider;
    public Image fetchSliderImage;
    public Color fetchInProgressColor = Color.red;
    public Color fetchDoneColor = Color.green;
    public int currentSeatNum = 0;

    public Dictionary<string, bool> answers = new Dictionary<string, bool>();
    public List<GameObject> answerDestinations;

    private bool reachedSeat = false;

    public void Update()
    {
        SetFetchUI();
        if (HasWon())
        {
            return;
        }
    }

    public void ReachedSeat()
    {
        reachedSeat = true;
    }

    public void LeftSeat()
    {
        reachedSeat = false;
    }

    public bool HasWon()
    {
        bool gotAnswers = true;
        foreach (bool gotAnswer in answers.Values)
        {
            gotAnswers = gotAnswer && gotAnswers;
        }
        return gotAnswers && reachedSeat;
    }

    public void Reset()
    {
        reachedSeat = false;
        currentSeatNum = 0;
        answers.Clear();
        foreach (GameObject desk in answerDestinations)
        {
            answers.Add(desk.GetComponent<DeskTrigger>().triggerName, false);
        }
    }

    public void SetAnswers()
    {
        foreach (GameObject desk in answerDestinations)
        {
            answers.Add(desk.GetComponent<DeskTrigger>().triggerName, false);
        }
    }

    private void OnEnable()
    {
        fetchProgress = 0.0f;
        SetFetchUI();
    }

    private void SetFetchUI()
    {
        fetchSlider.value = fetchProgress;
        fetchSliderImage.color = fetchProgress >= 100.0f ? Color.green : Color.red;
    }

}
