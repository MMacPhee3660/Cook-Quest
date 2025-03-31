using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    void Start()
    {
        // Initializes currentHealth to a specific value instead of MaxHealth
        currentHealth = 100; // Can be set to whatever value wanted
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(currentHealth);
    }
        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            healthBar.SetHealth(currentHealth);
        }
        
    private void Update()
    {
        Debug.Log(currentHealth);
         if (currentHealth > MaxHealth)
         {
           currentHealth = MaxHealth;
        }   
         if(currentHealth <=0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("You Died!");
        Destroy(gameObject);
    }
     public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
} 