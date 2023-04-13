using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private Health playerHealth;

    [SerializeField]
    private Heart[] hearts;
    [SerializeField]
    private GameObject[] boards;

    private void Start()
    {
        PlayerController playerController =
            FindAnyObjectByType<PlayerController>();

        playerHealth = playerController.GetComponent<Health>();
        playerHealth.RegisterOnHealthChanged(OnHealthChanged);

        OnHealthChanged(playerHealth.currentHealth, true);
    }

    private void OnHealthChanged(int newAmount, bool increased)
    {
        int MaxHealth = playerHealth.maxHealth;

        if (MaxHealth == 12)
        {
            SetBoard(0);
        }
        else if (MaxHealth == 16)
        {
            SetBoard(1);
        }
        else if (MaxHealth == 20)
        {
            SetBoard(2);
        }
        else if (MaxHealth == 24)
        {
            SetBoard(3);
        }
        else
        {
            SetBoard(0);
            Debug.Log("Should not happen unless there is a code logic mistake");
        }

        SetHearts(newAmount);
    }

    private void SetBoard(int boardIndex)
    {
        for (int i = 0; i < boards.Length; i++)
        {
            if (i == boardIndex)
            {
                boards[i].SetActive(true);
            }
            else
            {
                boards[i].SetActive(false);
            }
        }
    }

    private void SetHearts(int heartCount)
    {
        if (heartCount <= 0)
        {
            SetBoard(boards.Length - 1);
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
