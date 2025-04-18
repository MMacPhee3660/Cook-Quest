using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{   
    [Header("Item")]
    public Item item;


    public InventoryManager inventoryManager;
    
    public SpriteRenderer spriteRenderer;


    void Awake(){
        GameObject inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        this.spriteRenderer.sprite = item.image;

    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PickupItem(item.ID); 
            
        }
    }

    public void PickupItem(int id){
        Destroy(gameObject);
        inventoryManager.AddItem(inventoryManager.itemsToPickup[id]);
        
    }
}
