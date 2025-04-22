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
    public GameObject objectToDestroy;
   
    [field : SerializeField] public GameObject ResourceNode {get; private set;}
    [field: SerializeField] public ParticleSystem ps {get; private set;}
    private int amountHarvested = 0;
    public Boolean hasDropped = false;

    public int currentHealh;
    [field : SerializeField] public int maxHealth {get; private set;}   



    public void Update()
    {
    }


    public void Harvest(int amount)
    {
        int amountToSpawn = Mathf.Min(amount, maxHealth - amountHarvested);
        GetSelectedItem();

        switch (resourceType){
            case ResourceType.Rock:
                if(amountToSpawn > 0 && receivedItem.actionType == droppedItem.actionType && receivedItem.itemType == ItemType.Tool){
                ps.Emit(amountToSpawn);
                amountHarvested += amountToSpawn;
                animator.SetTrigger("hit");
                currentHealh -= amountToSpawn;

                Debug.Log("Max health: " + maxHealth);
                Debug.Log("Current health: " + currentHealh);

                if(amountHarvested >= maxHealth){
                    Destroy(this.gameObject);
                }
                }
            break;
            case ResourceType.Tree:
            if(amountToSpawn > 0 && receivedItem.actionType == droppedItem.actionType && receivedItem.itemType == ItemType.Tool){
                ps.Emit(amount);
                amountHarvested += amountToSpawn;
                animator.SetTrigger("hit");
                  if(amountHarvested >= maxHealth){
                    Destroy(this.gameObject);
                }
                }
            break;
            case ResourceType.Enemy:
            if(amountToSpawn > 0 && receivedItem.actionType == droppedItem.actionType && receivedItem.itemType == ItemType.Tool){
                toDrop = toDrop + 1;
                amountHarvested += amountToSpawn;
                // animator.SetTrigger("hit");
                if(amountHarvested >= maxHealth){
                    ps.Emit(toDrop);
                    toDrop = 0;
                    Destroy(this.gameObject);
                }
            }
            break;
        }

        
    }


 void Awake(){
        GameObject inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        currentHealh = maxHealth;
    }


    public void GetSelectedItem(){
        receivedItem = inventoryManager.GetSelectedItem();
    }

}
