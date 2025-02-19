using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    //void Update used for testing damage
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.PageDown))
        {
           TakeDamage(20);
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        Destroy(gameObject);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
} 