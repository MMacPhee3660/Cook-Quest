using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

 public Harvestable harvestable; // Reference to the Harvestable script if needed
 public EnemyHealthBarSlider healthBarSlider;
    void Start()
    {
        healthBarSlider.SetMaxEnemyHealth(harvestable.maxHealth);
        healthBarSlider.SetEnemyHealth(harvestable.currentHealh);
    }

    // public void TakeDamage(int damage)
    // {
    //     amountHarvested -= damage;
    //     if (amountHarvested < 0)
    //     {
    //         amountHarvested = 0;
    //     }
    //     healthBarSlider.SetEnemyHealth(amountHarvested);
    // }

    public void Heal(int amount)
    {
        harvestable.currentHealh += amount;
        if (harvestable.currentHealh > harvestable.maxHealth)
        {
            harvestable.currentHealh = harvestable.maxHealth;
        }
        healthBarSlider.SetEnemyHealth(harvestable.currentHealh);
    }
}
   