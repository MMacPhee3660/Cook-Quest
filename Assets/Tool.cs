using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collider){
        Harvestable harvestable = collider.GetComponent<Harvestable>();
        if (harvestable != null){
            harvestable.Harvest(1);
        }


    }
}
