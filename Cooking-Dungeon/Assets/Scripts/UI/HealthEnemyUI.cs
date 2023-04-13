using UnityEngine;

public class HealthEnemyUI : MonoBehaviour
{
    private Health enemyHealth;

    [SerializeField]
    private Heart[] hearts;

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

        for (int i = 0; i < hearts.Length; i++)
        {
            int amount;

            if (heartCount >= 4)
            {
                amount = 4;
                heartCount -= amount;
            }
            else if (heartCount == 3)
            {
                amount = 3;
                heartCount -= amount;
            }
            else if (heartCount == 2)
            {
                amount = 2;
                heartCount -= amount;
            }
            else if (heartCount == 1)
            {
                amount = 1;
                heartCount -= amount;
            }
            else
            {
                amount = 0;
                heartCount -= amount;
            }

            hearts[i].SetPortions(amount);

        }

    }

}
