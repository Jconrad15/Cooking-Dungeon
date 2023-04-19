using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject sword;

    public void SetCurrentAction(CombatAction action)
    {
        switch (action)
        {
            case CombatAction.Block:
                shield.SetActive(true);
                sword.SetActive(false);
                break;

            case CombatAction.Attack:
                shield.SetActive(false);
                sword.SetActive(true);
                break;
        }

    }

}
