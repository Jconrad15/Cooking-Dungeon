using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject deathParticles;

    // <amount, increased==true>
    private Action<int, bool> cbOnHealthChanged;

    // Max defaults to 6
    public int maxHealth = 6;
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

    private void Die()
    {
        // TODO: Add particle effect as a poof?
        GameObject particles = Instantiate(deathParticles);
        particles.transform.position = transform.position;
        particles.transform.rotation =
            Quaternion.LookRotation(transform.up);

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
