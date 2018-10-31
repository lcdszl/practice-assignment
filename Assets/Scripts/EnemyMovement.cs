using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody2D rb2D;
    public float speed = 2.0f;
    public Transform[] waypoints;

    public float upMod;
    public float rightMod;
	// Use this for initialization
	void OnEnable () {
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveRoutine());
	}
	

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > 0.1f && enabled)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, speed * Time.deltaTime);
            //upMod = newPosition.y - rb2D.position.y == 0f ? 0 : (int)(Mathf.Sign(newPosition.y - rb2D.position.y));
            //rightMod = newPosition.x - rb2D.position.x == 0f ? 0 : (int)(Mathf.Sign(newPosition.x - rb2D.position.x));
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    private IEnumerator MoveRoutine()
    {
        int i;
        for (i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                CalculateMods(waypoints[i]);
                yield return StartCoroutine(SmoothMovement(waypoints[i].position));
                ResetMod();
            }
            else
            {
                yield return null;
            }

        }

        for (int j = i-1; j >= 0; j--)
        {
            if (waypoints[j] != null)
            {
                CalculateMods(waypoints[j]);
                yield return StartCoroutine(SmoothMovement(waypoints[j].position));
                ResetMod();
            }
            else
            {
                yield return null;
            }

        }
        StartCoroutine(MoveRoutine());

    }

    public void CalculateMods(Transform dest)
    {
        Vector3 destVector = dest.position - transform.position;
        if (Mathf.Abs(destVector.x) > Mathf.Abs(destVector.y))
        {
            rightMod = destVector.normalized.x;
        }
        else
        {
            upMod = destVector.normalized.y;
        }
    }

    private void ResetMod()
    {
        upMod = 0;
        rightMod = 0;
    }
}
