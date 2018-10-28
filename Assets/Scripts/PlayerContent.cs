using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour {

    public float fetchSpeed = 30.0f;

    [Range(0.0f, 100.0f)]
    public float fetchProgress;

    public Slider fetchSlider;
    public Image fetchSliderImage;
    public Color fetchInProgressColor = Color.red;
    public Color fetchDoneColor = Color.green;

    private bool beenSeen = false;

    public Dictionary<string, bool> answers = new Dictionary<string, bool>() { { "desk0", false } };

    private bool reachedSeat = false;

    public void Update()
    {
        SetFetchUI();
    }

    public void HasBeenSeen()
    {
        beenSeen = true;
    }


    public void ReachedSeat()
    {
        reachedSeat = true;
    }

    public bool HasWon()
    {
        return true;
        //return (!beenSeen) && gotAnswer && reachedSeat;
    }

    public void Reset()
    {
        beenSeen = false;
        reachedSeat = false;
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
