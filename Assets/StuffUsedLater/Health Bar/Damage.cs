using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 2;
    public playerHealth playerHealth;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerHealth == null)
            {
            }
            playerHealth.TakeDamage(damage);
        }
    }

public void TakeDamage()
{
    playerHealth.health = playerHealth.health - damage;
}
}

