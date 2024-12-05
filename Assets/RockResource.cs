using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <=0){
            Destroy(gameObject);
        }
    }
}
