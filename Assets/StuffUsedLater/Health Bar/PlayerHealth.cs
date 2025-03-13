using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int MaxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }
        public void Heal(int amount)
    {
        currentHealth += amount;
        healthBar.SetMaxHealth(currentHealth);
    }
    private void Update()
    {
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
} 