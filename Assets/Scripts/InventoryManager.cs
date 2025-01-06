using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{


    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] itemsToPickup;

    public void AddItem(Item item){ 

        for(int i = 0; i < InventorySlots.Length; i++){
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && 
            itemInSlot.item == item && 
            itemInSlot.count < 10){
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return;
            }
        }

        for(int i = 0; i < InventorySlots.Length; i++){
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item, slot);
                return;
            }
        }

    }

    void SpawnNewItem( Item item, InventorySlot slot){
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
