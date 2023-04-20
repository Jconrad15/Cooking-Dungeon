using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private PlayerController playerController;
    private Combatant playerCombatant;

    private Action cbOnCombatDone;

    private Action cbOnBlock;
    private Action cbOnFailedBlock;
    private Action cbOnAttack;
    private Action cbOnFailedAttack;
    private Action<CombatAction, CombatAction> cbOnCurrentActionChanged;
    
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

        Queue<CombatAction> actions = otherCombatant.GetCombatActions();
        CombatAction currentAction = actions.Dequeue();
        CombatAction peek;
        if (actions.Count > 0)
        {
            peek = actions.Peek();
        }
        else
        {
            peek = CombatAction.Done;
        }
        cbOnCurrentActionChanged?.Invoke(currentAction, peek);

        // Combat goes here
        bool combatDone = false;
        while (combatDone == false)
        {
            if (CheckCombatOver(otherCombatant, currentAction))
            {
                combatDone = true;
                break;
            }

            if (CheckPlayerInput(otherCombatant, currentAction))
            {
                currentAction = actions.Dequeue();
                if (actions.Count > 0)
                {
                    peek = actions.Peek();
                }
                else
                {
                    peek = CombatAction.Done;
                }
                cbOnCurrentActionChanged?.Invoke(currentAction, peek);
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        Debug.Log("Combat Done");
        cbOnCombatDone?.Invoke();
        playerController.EnableMovement();
    }

    private bool CheckPlayerInput(
        Combatant otherCombatant, CombatAction currentAction)
    {
        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);
        if (leftClick)
        {
            TryBlock(otherCombatant, currentAction);
            return true;
        }
        else if (rightClick)
        {
            TryAttack(otherCombatant, currentAction);
            return true;
        }

        return false;
    }

    private void TryAttack(
        Combatant otherCombatant, CombatAction currentAction)
    {
        switch (currentAction)
        {
            case CombatAction.Block:
                // Tried to attack when needed to block
                cbOnFailedBlock?.Invoke();
                otherCombatant.Attack(playerCombatant);
                break;

            case CombatAction.Attack:
                // Tried to attack when needed to attack
                cbOnAttack?.Invoke();
                playerCombatant.Attack(otherCombatant);
                break;
        }
    }

    private void TryBlock(
        Combatant otherCombatant, CombatAction currentAction)
    {
        switch (currentAction)
        {
            case CombatAction.Block:
                // Tried to block when needed to block
                cbOnBlock?.Invoke();
                break;

            case CombatAction.Attack:
                // Tried to block when needed to attack
                cbOnFailedAttack?.Invoke();
                break;
        }
    }

    private bool CheckCombatOver(
        Combatant otherCombatant, CombatAction currentAction)
    {
        // Check if player or otherCombatant died
        if (currentAction == CombatAction.Done) { return true; }

        return playerCombatant == null || otherCombatant == null;
    }

    public void RegisterOnCombatDone(Action callbackfunc)
    {
        cbOnCombatDone += callbackfunc;
    }

    public void UnregisterOnCombatDone(Action callbackfunc)
    {
        cbOnCombatDone -= callbackfunc;
    }

    public void RegisterOnBlock(Action callbackfunc)
    {
        cbOnBlock += callbackfunc;
    }

    public void UnregisterOnBlock(Action callbackfunc)
    {
        cbOnBlock -= callbackfunc;
    }

    public void RegisterOnFailedBlock(Action callbackfunc)
    {
        cbOnFailedBlock += callbackfunc;
    }

    public void UnregisterOnFailedBlock(Action callbackfunc)
    {
        cbOnFailedBlock -= callbackfunc;
    }

    public void RegisterOnAttack(Action callbackfunc)
    {
        cbOnAttack += callbackfunc;
    }

    public void UnregisterOnAttack(Action callbackfunc)
    {
        cbOnAttack -= callbackfunc;
    }

    public void RegisterOnFailedAttack(Action callbackfunc)
    {
        cbOnFailedAttack += callbackfunc;
    }

    public void UnregisterOnFailedAttack(Action callbackfunc)
    {
        cbOnFailedAttack -= callbackfunc;
    }

    public void RegisterOnCurrentActionChanged(
        Action<CombatAction, CombatAction> callbackfunc)
    {
        cbOnCurrentActionChanged += callbackfunc;
    }

    public void UnregisterOnCurrentActionChanged(
        Action<CombatAction, CombatAction> callbackfunc)
    {
        cbOnCurrentActionChanged -= callbackfunc;
    }
}
