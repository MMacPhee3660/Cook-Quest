using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealingItem : MonoBehaviour
{
    private int health = 25;
    private int currentHealth;

    PlayerHealth playerHealth;
    public GameObject player;
    void Awake()
    {
        player = GameObject.Find("PlayerMove");
        playerHealth = player.GetComponent<PlayerHealth>();
        currentHealth = health;
    }
        public int amount;

    public HealthBar healthBar; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Heal(currentHealth);
            Destroy(gameObject);
        }
    }
}
