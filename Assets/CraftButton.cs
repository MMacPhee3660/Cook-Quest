using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class CraftButton : MonoBehaviour
{

    public Item productItem;
    public InventoryManager inventoryManager;
    GameObject inventoryManagerObj;
    [SerializeField]TextMeshProUGUI textToEdit;
    [SerializeField] Item[] itemsReq;
    [SerializeField] int[] amounts;
    public List<Tuple<InventoryItem, int>> slots = new List<Tuple<InventoryItem, int>>();
    
    void Start()
    {

    }
    void Awake(){
        inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        textToEdit.text = productItem.itemName;
    }

    // Update is called once per frame
 
    public void craft(){
        int pos =0;
        int haveItems = 0;
        foreach(Item currItem in itemsReq){
            for(int i = 0; i < inventoryManager.InventorySlots.Length; i++){
                InventorySlot slot = inventoryManager.InventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if(itemInSlot != null && itemInSlot.item == currItem && itemInSlot.count >= amounts[pos]){
                    haveItems += 1;
                    slots.Add(new Tuple<InventoryItem, int>(itemInSlot,amounts[pos]));
                 
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
