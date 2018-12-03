using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItemR2 : MonoBehaviour {

    private InventoryR2 inventory;
    public GameObject itemButton;
    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<InventoryR2>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < inventory.itemSlots.Length; i++)
            {
                if(inventory.isFull[i] == false){
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.itemSlots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }

        }
    }
}
