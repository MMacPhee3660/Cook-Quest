using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{   
    public InventoryManager inventoryManager;
    [Header("Type")]
    public Item item;

    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            inventoryManager.AddItem(item);
            Debug.Log("pickup");
        }
    }
}
