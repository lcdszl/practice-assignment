using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0f,360f)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask roomMask;

    public Dictionary<int, int> playersLastSeenSeat = new Dictionary<int, int>();
    public List<Transform> seenPlayers = new List<Transform>();

    public bool foundPlayer = false;

    private float upMod;
    private float rightMod;
    public Vector3 currentForward;

    public void OnEnable()
    {
        StartCoroutine(CheckForPlayer());
    }

    public IEnumerator CheckForPlayer()
    {
        while (!foundPlayer)
        {
            yield return null;
            FindVisibleTarget();
        }
    }

    public Vector2 DirFromAngle(float angleDegree)
    {

        angleDegree += CalcAngle();
        return new Vector2(Mathf.Sin(angleDegree * Mathf.Deg2Rad), Mathf.Cos(angleDegree * Mathf.Deg2Rad));
    }

    private float CalcAngle()
    {
        upMod = GetComponent<EnemyMovement>().upMod;
        rightMod = GetComponent<EnemyMovement>().rightMod;
        float result = 0;
        Vector3 forward = new Vector3(0, 1, 0);
        if (upMod < 0)
        {
            forward = new Vector3(0, -1, 0);
            result += upMod * 180f;
        }
        else if (rightMod != 0)
        {
            forward = new Vector3(rightMod, 0, 0).normalized;
            result += rightMod * 90f;
        }
        currentForward = forward;
        return result;

    }

    public void FindVisibleTarget()
    {
        seenPlayers.Clear();
        Collider2D[] sawObjects = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < sawObjects.Length; i++)
        {
            float offset = CalcAngle();
            Vector3 dirToTarget = (sawObjects[i].transform.position - transform.position).normalized;
            if (Vector3.Angle(currentForward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(sawObjects[i].transform.position, transform.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, disToTarget, roomMask))
                {
                    SetPlayerAsSeen(sawObjects[i]);
                    seenPlayers.Add(sawObjects[i].transform);
                }
            }
        }
    }

    public void SetPlayerAsSeen(Collider2D playerObject)
    {
        int currentSeat = playerObject.GetComponent<PlayerContent>().currentSeatNum;
        int playerNum = playerObject.GetComponent<PlayerContent>().playerNum;
        if (currentSeat == 0 )
        {
            GameOver();
        }
        else if (!playersLastSeenSeat.ContainsKey(playerNum))
        {
            playersLastSeenSeat.Add(playerNum, currentSeat);
        }
        else if (playersLastSeenSeat[playerNum] != currentSeat)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        foundPlayer = true;
    }

}
