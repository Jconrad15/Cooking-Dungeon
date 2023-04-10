using TMPro;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject combatUI;

    // Start with combatBox off
    private void Start()
    {
        FindAnyObjectByType<PlayerController>().
            RegisterOnStartCombat(NewCombat);

        combatUI.SetActive(false);
    }

    public void NewCombat(Combatant otherCombatant)
    {
        ShowCombatUI();
        ShowText(otherCombatant);
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

}
