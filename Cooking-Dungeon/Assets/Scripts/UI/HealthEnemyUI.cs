using UnityEngine;

public class HealthEnemyUI : MonoBehaviour
{
    private Health enemyHealth;

    [SerializeField]
    private GameObject[] hearts;

    private void Start()
    {
        enemyHealth = GetComponentInParent<Health>();
        enemyHealth.RegisterOnHealthChanged(OnHealthChanged);
        OnHealthChanged(enemyHealth.currentHealth, true);
    }

    private void OnHealthChanged(int newAmount, bool increased)
    {
        SetHearts(newAmount);
    }

    private void SetHearts(int heartCount)
    {
        if (heartCount <= 0)
        {
            return;
        }

        for (int i = 0; i < heartCount; i++)
        {
            hearts[i].SetActive(true);
        }
        for (int i = heartCount; i < hearts.Length; i++)
        {
            hearts[i].SetActive(false);
        }
    }

}
