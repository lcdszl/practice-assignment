using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float velocity = 10f;

	// Use this for initialization
	void OnEnable () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

    private void OnDisable()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    void FixedUpdate () {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(h * Mathf.Abs(Mathf.Cos(Mathf.Atan2(v, h))), v * Mathf.Abs(Mathf.Sin(Mathf.Atan2(v, h))));

        rb2d.velocity = movement * velocity;
	}



}
