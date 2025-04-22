using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public InventoryManager inventoryManager;
    Item item;
    public void Awake()
    {
        GameObject inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        
    }

    private void OnTriggerEnter(Collider collider){
        item = inventoryManager.GetSelectedItem();
        Harvestable harvestable = collider.GetComponent<Harvestable>();
        if (harvestable != null){
            harvestable.Harvest(item.power);
        }


    }
}
