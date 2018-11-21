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

    public GameObject upInd;
    public GameObject downInd;
    public GameObject rightInd;
    public GameObject leftInd;

    public float blinkDuration = 0.2f;
    public DirectionEnum.Directions enemyDirection;

    public void Update()
    {
        SetFetchUI();
    }

    private IEnumerator EnemyIndicatorLoop()
    {
        while (true)
        {
            switch (enemyDirection)
            {
                case DirectionEnum.Directions.Up:
                    upInd.SetActive(true);
                    break;
                case DirectionEnum.Directions.Down:
                    downInd.SetActive(true);
                    break;
                case DirectionEnum.Directions.Right:
                    rightInd.SetActive(true);
                    break;
                case DirectionEnum.Directions.Left:
                    leftInd.SetActive(true);
                    break;
                case DirectionEnum.Directions.None:
                    break;
            }
            yield return new WaitForSeconds(blinkDuration);
            upInd.SetActive(false);
            downInd.SetActive(false);
            rightInd.SetActive(false);
            leftInd.SetActive(false);
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public bool ReachedHomeSeat()
    {
        return currentSeatNum == homeSeat.GetComponent<SeatTrigger>().seatNum;
    }

    public virtual bool HasWon()
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
        enemyDirection = DirectionEnum.Directions.None;
        currentSeatNum = 0;
        answers.Clear();
        SetAnswers();
        StartCoroutine(EnemyIndicatorLoop());
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
