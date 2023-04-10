using System;
using System.Collections;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private readonly KeyCode attackButton = KeyCode.Space;

    private PlayerController playerController;
    private Combatant playerCombatant;

    private Action cbOnCombatDone;

    // Start with combatBox off
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerController.RegisterOnStartCombat(NewCombat);

        playerCombatant = playerController.GetComponent<Combatant>();
    }

    public void NewCombat(Combatant otherCombatant)
    {
        playerController.DisableMovement();
        StartCoroutine(Combat(otherCombatant));
    }

    private IEnumerator Combat(Combatant otherCombatant)
    {
        yield return new WaitForSeconds(0.1f);

        // Combat goes here
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
                done = true;
                break;
            }

            if (Input.GetKeyDown(attackButton))
            {
                Debug.Log("AttackButton");
                playerCombatant.Attack(otherCombatant);
                yield return new WaitForEndOfFrame();

                // TODO: Combat
                // for now, the other combatant
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
        //Debug.Log("Combat Done");
        cbOnCombatDone?.Invoke();
        playerController.EnableMovement();
    }

    public void RegisterOnCombatDone(Action callbackfunc)
    {
        cbOnCombatDone += callbackfunc;
    }

    public void UnregisterOnCombatDone(Action callbackfunc)
    {
        cbOnCombatDone -= callbackfunc;
    }
}
