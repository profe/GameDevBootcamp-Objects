using System;
using UnityEngine;

public class Health
{
    //fields
    private float currentHealth;
    private float maxHealth;
    private float healthRegenRate;

    //properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            OnHealthUpdate?.Invoke(currentHealth); //invoke C# action whenever there's a change
        }
    }

    //event handling
    public Action<float> OnHealthUpdate;


    public Health(float maxHealth, float healthRegenRate, float currentHealth = 100)
    {
        this.CurrentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.healthRegenRate = healthRegenRate;
    }

    public Health(float maxHealth) : this(maxHealth, 0, 0) { }

    public Health() : this(1, 0, 0) { }

    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + value);
    }

    public void DeductHealth(float value)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - value);
    }

    public void RegenHealth()
    {
        AddHealth(healthRegenRate * Time.deltaTime);
    }

    public void FullHeal()
    {
        CurrentHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }
}
