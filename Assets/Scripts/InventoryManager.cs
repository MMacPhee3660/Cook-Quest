using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{


    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] itemsToPickup;
    public Item[] startItems;

    public int selectedSlot = -1;
    
    void ChangeSelectedSlot(int newValue){
        if(selectedSlot >=0){
        InventorySlots[selectedSlot].Deselect();
        }
        InventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public void Start(){
        ChangeSelectedSlot(0);
        foreach (var item in startItems){
            AddItem(item);  
        }
    }

    public void Update(){
        if(Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 6){
                ChangeSelectedSlot(number -1);
            }
        }
    }

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
     public static GameObject instance;
    void Awake()
    {
        if(instance == null){
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance !=this){
            Destroy(gameObject);
        }
    }

    public Item GetSelectedItem(){
        InventorySlot slot = InventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            return itemInSlot.item;
        }
        return null;
    }
}
