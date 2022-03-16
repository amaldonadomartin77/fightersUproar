using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;

    public int baseHealth;

    private int healthAmount;
    private int healthAmountMax;

    public void Start()
    {
        healthAmountMax = baseHealth;
        healthAmount = baseHealth;
    }

    public void Damage(int amount)
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
}