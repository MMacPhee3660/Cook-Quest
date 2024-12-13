using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{


    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;

    public void AddItem(Item item){ 

        for(int i = 0; i < InventorySlots.Length; i++){
            InventorySlot slot = InventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if(itemInSlot == null){
                SpawnNewItem(item, slot);
                return;
            }
        }

    }

    void SpawnNewItem( Item item, InventorySlot slot){
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem inventoryItem = newItemGo.GetComponent<DraggableItem>();
        inventoryItem.InitialiseItem(item);
    }
}
