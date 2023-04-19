using TMPro;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject combatUI;
    [SerializeField]
    private CombatActionDisplayer combatActionDisplayer;

    // Start with combatBox off
    private void Start()
    {
        FindAnyObjectByType<PlayerController>()
            .RegisterOnStartCombat(NewCombat);

        CombatSystem cs = FindAnyObjectByType<CombatSystem>();
        cs.RegisterOnCombatDone(OnCombatDone);
        cs.RegisterOnCurrentActionChanged(OnCurrentActionChanged);



        combatUI.SetActive(false);
    }

    private void NewCombat(Combatant otherCombatant)
    {
        ShowCombatUI();
        ShowText(otherCombatant);
    }

    private void OnCombatDone()
    {
        HideCombatUI();
    }

    private void ShowText(Combatant otherCombatant)
    {
        text.SetText(otherCombatant.combatantName);
    }

    private void ShowCombatUI()
    {
        combatUI.SetActive(true);
    }

    private void HideCombatUI()
    {
        combatUI.SetActive(false);
    }

    private void OnCurrentActionChanged(CombatAction action)
    {
        //Debug.Log(action.ToString());
        combatActionDisplayer.SetCurrentAction(action);
    }

}
