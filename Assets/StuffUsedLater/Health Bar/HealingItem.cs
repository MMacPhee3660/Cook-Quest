using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealingItem : MonoBehaviour
{
    public int health;
    public int currentHealth;

    PlayerHealth playerHealth;
    public GameObject player;
    void Awake()
    {
        player = GameObject.Find("PlayerMove");
        playerHealth = player.GetComponent<PlayerHealth>();
        currentHealth = health;
    }

    public HealthBar healthBar; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = GameObject.Find("PlayerMove").GetComponent<PlayerHealth>();
            playerHealth.Heal(25);
            healthBar.SetMaxHealth(currentHealth);
            Debug.Log("Player healed");
            Destroy(gameObject);
        }
    }
}
