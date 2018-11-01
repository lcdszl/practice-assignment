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
    public GameObject homeSeat;

    public void Update()
    {
        SetFetchUI();
    }

    public bool ReachedHomeSeat()
    {
        return currentSeatNum == homeSeat.GetComponent<SeatTrigger>().seatNum;
    }

    public bool HasWon()
    {
        bool gotAnswers = answers.Count > 0 ? true : false;
        foreach (bool gotAnswer in answers.Values)
        {
            gotAnswers = gotAnswer && gotAnswers;
        }
        return gotAnswers && ReachedHomeSeat();
    }

    public void Reset()
    {
        currentSeatNum = 0;
        answers.Clear();
        SetAnswers();
    }

    public void SetAnswers()
    {
        if (answerDestinations == null) return;
        foreach (GameObject desk in answerDestinations)
        {
            if (desk != null)
            {
                answers.Add(desk.GetComponent<DeskTrigger>().triggerName, false);
            }
            
        }
    }

    private void OnEnable()
    {
        fetchProgress = 0.0f;
        SetFetchUI();
        Reset();
    }

    private void SetFetchUI()
    {
        fetchSlider.value = fetchProgress;
        fetchSliderImage.color = fetchProgress >= 100.0f ? Color.green : Color.red;
    }

}
