using UnityEngine;
public class EnemyHealthBar : MonoBehaviour
{
    public Harvestable harvestable; // Reference to the Harvestable script if needed
    public EnemyHealthBarSlider healthBarSlider;

    void Awake()
    {
        if (harvestable == null)
            harvestable = GetComponentInParent<Harvestable>();
        if (healthBarSlider == null)
            healthBarSlider = GetComponentInChildren<EnemyHealthBarSlider>();
    }

    void Start()
    {
        healthBarSlider.SetMaxEnemyHealth(harvestable.maxHealth);
        healthBarSlider.SetEnemyHealth(harvestable.currentHealh);
    }

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
   