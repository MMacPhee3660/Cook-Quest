using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftButton : MonoBehaviour
{

    public Item productItem;
    public InventoryManager inventoryManager;
    GameObject inventoryManagerObj;

    [SerializeField] Item[] itemsReq;
    [SerializeField] int[] amounts;
    public List<Tuple<InventoryItem, int>> slots = new List<Tuple<InventoryItem, int>>();
    
    void Start()
    {
        
    }
    void Awake(){
        inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
 
    public void craft(){
        int pos =0;
        int haveItems = 0;
        foreach(Item currItem in itemsReq){
            Debug.Log(currItem);
            for(int i = 0; i < inventoryManager.InventorySlots.Length; i++){
                InventorySlot slot = inventoryManager.InventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if(itemInSlot != null && itemInSlot.item == currItem && itemInSlot.count >= amounts[pos]){
                    haveItems += 1;
                    Debug.Log(haveItems);
                    slots.Add(new Tuple<InventoryItem, int>(itemInSlot,amounts[pos]));
                    Debug.Log("i got past");
                }
            }
            pos += 1;
        }
        if(haveItems == itemsReq.Length){
            inventoryManager.AddItem(productItem);
            foreach(var slot in slots){
                slot.Item1.count -= slot.Item2;
                slot.Item1.RefreshCount();
                if(slot.Item1.count <= 0){
                    Destroy(slot.Item1.gameObject);
                }
            }
            slots.Clear();
        }
    }
}
// itemInSlot.count -= amounts[pos];
//                     itemInSlot.RefreshCount();
//                     Debug.Log(itemInSlot.count);
//                     if(itemInSlot.count <= 0){
//                         Destroy(itemInSlot.gameObject);
//                         Destroy(itemInSlot.image);
                        
//                     }