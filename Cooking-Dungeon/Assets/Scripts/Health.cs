using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // max defaults to 6
    public int maxHealth = 6;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
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
        }
    }

    private void Die()
    {
        // TODO: Add particle effect as a poof?

        Destroy(gameObject);
    }
}
