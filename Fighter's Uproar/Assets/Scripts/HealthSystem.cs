using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;

    public int baseHealth;

    private float healthAmount;
    private int healthAmountMax;

    public void Start()
    {
        ResetHealth();
    }
    
    public void Damage(float amount)
    {
        healthAmount -= amount;
        if (healthAmount < 0)
            healthAmount = 0;
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public float GetCurrentHealth()
    {
        return healthAmount;
    }

    public void ResetHealth()
    {
        healthAmountMax = baseHealth;
        healthAmount = baseHealth;
    }

    public void SetHealth(float health)
    {
        healthAmount = health;
    }
}
