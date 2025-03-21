using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int damageTaken;
    public float speed;
    public Transform target;
    public int damage;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            int MaxHealth = playerHealth.MaxHealth;
            currentHealth = MaxHealth;
            healthBar.SetMaxHealth(MaxHealth);
        }
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    

    public void OnTriggerEnter(Collider other)
    {
   if (other.tag == "Player")
         {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(25);
            }
         }
    }
}

  

