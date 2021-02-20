using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private PlayerInventory inventory;
    public GameObject itemIcon;

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //item can be added to inventory
                    inventory.isFull[i] = true;       
                    Instantiate(itemIcon, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
