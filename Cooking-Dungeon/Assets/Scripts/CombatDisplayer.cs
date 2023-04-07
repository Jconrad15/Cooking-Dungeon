using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatDisplayer : MonoBehaviour
{
    private KeyCode attackButton = KeyCode.Space;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject combatUI;

    private PlayerController playerController;
    private Combatant playerCombatant;

    // Start with combatBox off
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerController.RegisterOnStartCombat(NewCombat);

        playerCombatant = playerController.GetComponent<Combatant>();

        combatUI.SetActive(false);
    }

    public void NewCombat(Combatant otherCombatant)
    {
        playerController.DisableMovement();
        ShowCombatUI();
        StartCoroutine(Combat(otherCombatant));
    }

    private IEnumerator Combat(Combatant otherCombatant)
    {
        ShowText(otherCombatant.combatantName);
        yield return new WaitForSeconds(0.1f);

        // TODO: combat goes here
        bool done = false;
        while (done == false)
        {
            // Check if otherCombatant died
            if (otherCombatant == null)
            {
                done = true;
                break;
            }

            // Check if player died
            if (playerCombatant == null)
            {
                Debug.Log("Player Died");
                // TODO: Gameover screen
            }

            /*if (Input.GetKeyDown(KeyCode.Escape))
            {
                done = true;
                break;
            }*/

            if (Input.GetKeyDown(attackButton))
            {
                Debug.Log("AttackButton");
                playerCombatant.Attack(otherCombatant);
                yield return new WaitForSeconds(0.25f);
                // TODO: FOR NOW, the other combatant
                // attacks player after being attacked
                // Check if otherCombatant died
                if (otherCombatant == null)
                {
                    done = true;
                    break;
                }
                otherCombatant.Attack(playerCombatant);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
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
