using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float velocity = 10f;
    //181106LIYUX
    private Animator playerAnimation; 
    //181106
	// Use this for initialization
	void OnEnable () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //181106LIYUX
        playerAnimation = GetComponent<Animator>();
        //181106
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
        //181106LIYUX
        playerAnimation.SetFloat("Speed",Mathf.Abs(rb2d.velocity.x));
        if(h > 0){
            transform.localScale = new Vector2(3f,3f);
        }
        else if(h < 0){
            transform.localScale = new Vector2(-3f,3f);
        }

        //181106
     
	}


   


}
