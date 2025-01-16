using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 2;
    private Health playerHealth;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerHealth == null)
            {
            playerHealth = collision.gameObject.GetComponent<Health>();
            }
            playerHealth.TakeDamage(damage);
        }
    }
}

internal class Health
{
    internal void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}