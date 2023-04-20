using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject currentSword;
    [SerializeField]
    private GameObject nextSword;

    [SerializeField]
    private GameObject currentShield;
    [SerializeField]
    private GameObject nextShield;

    private void Start()
    {
        TurnOffAll();
    }

    private void TurnOffAll()
    {
        currentSword.SetActive(false);
        nextSword.SetActive(false);
        nextShield.SetActive(false);
        currentShield.SetActive(false);
    }

    public void SetActions(
        CombatAction action, CombatAction nextAction)
    {
        TurnOffAll();

        switch (action)
        {
            case CombatAction.Block:
                currentShield.SetActive(true);
                break;

            case CombatAction.Attack:
                currentSword.SetActive(true);
                break;

            case CombatAction.Done:
                break;
        }

        switch (nextAction)
        {
            case CombatAction.Block:
                nextShield.SetActive(true);
                break;

            case CombatAction.Attack:
                nextSword.SetActive(true);
                break;

            case CombatAction.Done:
                break;
        }
    }



}
