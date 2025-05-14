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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (item != null && inventoryManager != null)
            {
                PickupItem(item.ID);
            }
            else
            {
                Debug.LogWarning("Item or InventoryManager is not assigned on " + gameObject.name);
            }
        }
    }

    public void PickupItem(int id){
        if (id >= 0 && id < inventoryManager.itemsToPickup.Length) {
            inventoryManager.AddItem(inventoryManager.itemsToPickup[id]);
            Destroy(gameObject);
        } else {
            Debug.LogError("Invalid item id: " + id);
        }
    }
}
