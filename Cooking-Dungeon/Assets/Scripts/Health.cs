using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject deathParticles;

    // <amount, increased==true>
    private Action<int, bool> cbOnHealthChanged;

    // Max defaults to 12
    public int maxHealth = 12;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        cbOnHealthChanged?.Invoke(currentHealth, true);
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        cbOnHealthChanged?.Invoke(currentHealth, false);
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
            cbOnHealthChanged?.Invoke(currentHealth, true);
        }
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        if (maxHealth >= 24)
        {
            maxHealth = 24;
        }
    }

    private void Die()
    {
        // Different death processing for player
        if (TryGetComponent(out PlayerController pc))
        {
            FindAnyObjectByType<PlayerDeathTrigger>().PlayerDied(pc);
            return;
        }

        // Particle effect
        GameObject particles = Instantiate(deathParticles);
        particles.transform.position = transform.position;
        particles.transform.rotation =
            Quaternion.LookRotation(transform.up);

        // Check if this character drops an ingredient on death
        if (TryGetComponent(out Combatant c))
        {
            if (c.droppedIngredientPrefab != null)
            {
                GameObject ingredient = Instantiate(
                    c.droppedIngredientPrefab);
                ingredient.transform.position = transform.position;
                ingredient.GetComponent<FaceCamera>()
                    .SetIsOnSurface(false);
            }
        }

        Destroy(gameObject);
    }

    public void RegisterOnHealthChanged(Action<int, bool> callbackfunc)
    {
        cbOnHealthChanged += callbackfunc;
    }

    public void UnregisterOnHealthChanged(Action<int, bool> callbackfunc)
    {
        cbOnHealthChanged -= callbackfunc;
    }
}
