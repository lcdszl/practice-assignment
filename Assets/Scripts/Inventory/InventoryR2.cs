using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryR2 : MonoBehaviour {

   
    public const int numItemSlots = 4;
    public bool[] isFull = new bool[numItemSlots];
    public GameObject[] itemSlots = new GameObject[numItemSlots];
}
