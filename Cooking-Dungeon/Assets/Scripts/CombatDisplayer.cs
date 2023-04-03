using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatDisplayer : MonoBehaviour
{
    private KeyCode attackButton = KeyCode.R;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject combatUI;

    private PlayerController playerController;

    // Start with combatBox off
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerController.RegisterOnStartCombat(NewCombat);
        combatUI.SetActive(false);
    }

    public void NewCombat(Combatant otherCombatant)
    {
        playerController.DisableMovement();
        ShowCombatUI();
        StartCoroutine(DisplayConversation(otherCombatant));
    }

    private IEnumerator DisplayConversation(Combatant otherCombatant)
    {
        ShowText(otherCombatant.combatantName);
        yield return new WaitForSeconds(1);

        // TODO: combat goes here

        Debug.Log("Combat Done");
        HideCombatUI();
        playerController.EnableMovement();
    }

    private void ShowText(string message)
    {
        text.SetText(message);
    }

    private void ShowCombatUI()
    {
        combatUI.SetActive(true);
    }

    private void HideCombatUI()
    {
        combatUI.SetActive(false);
    }

}
