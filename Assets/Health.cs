using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HealthChangeHandler(int oldHealth, int newHealth);
    public int maxHealth;
    public event HealthChangeHandler HealthChanged;

    public static int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        SetHealth(currentHealth - damage);
    }

    public void Heal(int heal)
    {
        SetHealth(currentHealth + heal);
    }

    public void SetHealth(int health)
    {
        var newHealth = Math.Max(0, Math.Min(maxHealth, health));

        HealthChanged(currentHealth, newHealth);

        currentHealth = newHealth;
    }
}
