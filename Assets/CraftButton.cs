using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{

    public Item productItem;
    public InventoryManager inventoryManager;
    GameObject inventoryManagerObj;

    [SerializeField] Item[] itemsReq;
    [SerializeField] int[] amounts;
    
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
        foreach(Item currItem in itemsReq){
            Debug.Log(currItem);
            pos += 1;
            for(int i = 0; i < inventoryManager.InventorySlots.Length; i++){
                Debug.Log(inventoryManager.InventorySlots.Length);
                InventorySlot slot = inventoryManager.InventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if(itemInSlot == currItem && itemInSlot.count >= amounts[pos]){
                Debug.Log(itemInSlot.count);
            }
        }

        }
    }
}
