using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public InventoryManager inventoryManager;
    Item item;
    public Animator animator;
    public void Awake()
    {
         animator = GetComponent<Animator>();
        GameObject inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        
    }

    private void OnTriggerEnter(Collider collider){
        for(int i =0; i <1; i++){
            item = inventoryManager.GetSelectedItem();
            Harvestable harvestable = collider.GetComponent<Harvestable>();
            if (harvestable != null){
                    harvestable.Harvest(item.power);
                    Debug.Log("hit");
                }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            animator.SetTrigger("Swing");
        }
    }
}
