using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Action<int> cbOnHealthChanged;

    // Max defaults to 6
    public int maxHealth = 6;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        cbOnHealthChanged?.Invoke(currentHealth);
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        cbOnHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            cbOnHealthChanged?.Invoke(currentHealth);
        }
    }

    private void Die()
    {
        // TODO: Add particle effect as a poof?

        Destroy(gameObject);
    }

    public void RegisterOnHealthChanged(Action<int> callbackfunc)
    {
        cbOnHealthChanged += callbackfunc;
    }

    public void UnregisterOnHealthChanged(Action<int> callbackfunc)
    {
        cbOnHealthChanged -= callbackfunc;
    }
}
