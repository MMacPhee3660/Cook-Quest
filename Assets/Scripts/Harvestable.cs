 using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;



public class Harvestable : MonoBehaviour
{
    public enum ResourceType{Rock, Tree, Enemy}
    [SerializeField] ResourceType resourceType;
    public InventoryManager inventoryManager;
    [SerializeField] Animator animator;
    Item receivedItem; //the item that is currently selected in the players inventory
    public Item droppedItem; //the item that the resource drops, used to check if the tool type is equal to the resource type (set in inspector currently)
    public int toDrop = 0;

    [field : SerializeField] public int ResourceCount {get; private set;}
    [field : SerializeField] public GameObject ResourceNode {get; private set;}
    [field: SerializeField] public ParticleSystem ps {get; private set;}
    private int amountHarvested = 0;
    public void Harvest(int amount)
    {
        int amountToSpawn = Mathf.Min(amount, ResourceCount - amountHarvested);
        GetSelectedItem();
        Debug.Log(receivedItem.actionType + " " + droppedItem.actionType);

        switch (resourceType){
            case ResourceType.Rock:
                if(amountToSpawn > 0 && receivedItem.actionType == droppedItem.actionType && receivedItem.itemType == ItemType.Tool){
                ps.Emit(amount);
                amountHarvested += amountToSpawn;
                animator.SetTrigger("hit");
                }
            break;
            case ResourceType.Tree:
            if(amountToSpawn > 0 && receivedItem.actionType == droppedItem.actionType && receivedItem.itemType == ItemType.Tool){
                ps.Emit(amount);
                amountHarvested += amountToSpawn;
                animator.SetTrigger("hit");
                }
            break;
            case ResourceType.Enemy:
            if(amountToSpawn > 0 && receivedItem.actionType == droppedItem.actionType && receivedItem.itemType == ItemType.Tool){
                toDrop = toDrop + 1;
                amountHarvested += amountToSpawn;
                // animator.SetTrigger("hit");
                if(amountHarvested >= ResourceCount){
                    ps.Emit(toDrop);
                    toDrop = 0;
                }
            }
            break;
        }

        if(amountHarvested >= ResourceCount){

            Destroy(gameObject);
        }
    }


 void Awake(){
        GameObject inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
    }


    public void GetSelectedItem(){
        receivedItem = inventoryManager.GetSelectedItem();
        if(receivedItem != null){
            Debug.Log("Received item:" + receivedItem);
        }
        else{
            Debug.Log("Did not receive item!");
        }
    }

}
