 using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Harvestable : MonoBehaviour
{

    [SerializeField] Animator animator;

    [field : SerializeField] public int ResourceCount {get; private set;}
    [field : SerializeField] public GameObject ResourceNode {get; private set;}
    [field: SerializeField] public ParticleSystem ps {get; private set;}
    private int amountHarvested = 0;
    public void Harvest(int amount)
    {
        int amountToSpawn = Mathf.Min(amount, ResourceCount - amountHarvested);

        if(amountToSpawn > 0){
            ps.Emit(amount);
            amountHarvested += amountToSpawn;
            animator.SetTrigger("hit");
        }

        if(amountHarvested >= ResourceCount){

            Destroy(gameObject);

        }
        
    }
}
