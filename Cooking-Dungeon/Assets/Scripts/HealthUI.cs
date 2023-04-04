using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    private void Start()
    {
        PlayerController playerController =
            FindAnyObjectByType<PlayerController>();

        Health health = playerController.GetComponent<Health>();
        health.RegisterOnHealthChanged(OnHealthChanged);

        OnHealthChanged(health.currentHealth, true);
    }

    private void OnHealthChanged(int newAmount, bool increased)
    {
        healthText.SetText(newAmount.ToString());
    }
}
