using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float dampTime = 0.1f;


    // private Camera playerCamera;
    private Transform target;
    private Vector3 refMoveVelocity;

    private void Awake()
    {
        if (PlayerList.players.Count != 0)
        {
            target = PlayerList.players[0].transform;
            PlayerList.players.Clear();
        }      
      //  playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    private void FixedUpdate () {
        CheckCameraTarget();
        MoveCamera();
	}

    private void MoveCamera()
    {
        if (target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref refMoveVelocity, dampTime);
        }      
    }

    private void CheckCameraTarget()
    {
        if (target == null)
        {
            if (PlayerList.players.Count != 0)
            {
                target = PlayerList.players[0].transform;
                PlayerList.players.Clear();
            }
        }
    }

}
