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

        playerController.GetComponent<Health>()
            .RegisterOnHealthChanged(OnHealthChanged);
    }

    private void OnHealthChanged(int newAmount)
    {
        healthText.SetText(newAmount.ToString());
    }
}
