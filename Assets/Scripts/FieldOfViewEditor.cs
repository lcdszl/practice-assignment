//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor (typeof(FieldOfView))]
//public class FieldOfViewEditor : Editor {

//    private void OnSceneGUI()
//    {
//        FieldOfView fow = (FieldOfView)target;
//        Handles.color = Color.black;
//        Handles.DrawWireArc(fow.transform.position, new Vector3(0, 0, 1), new Vector3(0, 1, 0), 360f, fow.viewRadius);

//        Vector3 dirA = fow.DirFromAngle(fow.viewAngle / 2);
//        Vector3 dirB = fow.DirFromAngle(-(fow.viewAngle / 2));
//        Handles.DrawLine(fow.transform.position, fow.transform.position + dirA * fow.viewRadius);
//        Handles.DrawLine(fow.transform.position, fow.transform.position + dirB * fow.viewRadius);

//        Handles.color = Color.red;
//        foreach (Transform seenPlayer in fow.seenPlayers)
//        {
//            Handles.DrawLine(fow.transform.position, seenPlayer.position);
//        }
//    }

//}
