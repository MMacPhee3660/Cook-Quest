using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health;
    public float maxhealth;
    public Image HealthBar;
    void Start()
    {
        maxhealth = health;
    }

    void Update()
    {
        HealthBar.fillAmount = Mathf.Clamp(health / maxhealth, 0,1);
    }
}
