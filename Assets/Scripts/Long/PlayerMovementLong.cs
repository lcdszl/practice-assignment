using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLong : PlayerMovement
{
    public override void FixedUpdate()
    {

#if UNITY_STANDALONE || UNITY_STANDALONE_OSX

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

#elif UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
        float v = 0;
        float h = 0;
        if (Input.touchCount > 0){
            Touch myTouch = Input.touches[0];
            touchPoint = myTouch.position;
            v = touchPoint.y - (Screen.height / 3);
            h = touchPoint.x - (Screen.width / 2);
        }
#endif
        v = Mathf.Max(v, 0);

        Vector2 movement = Vector3.Normalize(new Vector2(h * Mathf.Abs(Mathf.Cos(Mathf.Atan2(v, h))), v * Mathf.Abs(Mathf.Sin(Mathf.Atan2(v, h))))) ;

        rb2d.velocity = movement * velocity;
        playerAnimation.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
    }
}
