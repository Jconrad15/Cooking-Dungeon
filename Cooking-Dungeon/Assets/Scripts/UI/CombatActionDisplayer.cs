using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject combatActionPrefab;

    [SerializeField]
    private Sprite shield;
    [SerializeField]
    private Sprite sword;

    private GameObject previousAction;
    private GameObject previousNextAction;

    public void SetActions(
        CombatAction action, CombatAction nextAction)
    {
        Destroy(previousAction);
        MoveActions(action);
        CreateNewNextAction(nextAction);
    }

    private void MoveActions(CombatAction action)
    {
        if (previousNextAction == null)
        {
            previousNextAction = Instantiate(combatActionPrefab, transform);
        }

        SetSprite(previousNextAction, action);

        Animator a = previousNextAction.GetComponent<Animator>();
        a.SetTrigger("Next");
        previousAction = Instantiate(previousNextAction);
    }

    private void CreateNewNextAction(CombatAction nextAction)
    {
        Destroy(previousNextAction);
        previousNextAction = Instantiate(combatActionPrefab, transform);
        SetSprite(previousNextAction, nextAction);
    }

    private void SetSprite(GameObject GO, CombatAction a)
    {
        switch (a)
        {
            case CombatAction.Block:
                GO.GetComponent<Image>().sprite = shield;
                break;

            case CombatAction.Attack:
                GO.GetComponent<Image>().sprite = sword;
                break;

            case CombatAction.Done:
                GO.GetComponent<Image>().sprite = null;
                break;
        }
    }
}
