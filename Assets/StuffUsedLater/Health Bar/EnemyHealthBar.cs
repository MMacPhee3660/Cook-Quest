using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
 public int maxHealth = 100;
 public int currentHealth;
 public EnemyHealthBarSlider healthBarSlider;
    void Start()
    {
        currentHealth = maxHealth;
        healthBarSlider.SetMaxEnemyHealth(maxHealth);
        healthBarSlider.SetEnemyHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthBarSlider.SetEnemyHealth(currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBarSlider.SetEnemyHealth(currentHealth);
    }
}
   