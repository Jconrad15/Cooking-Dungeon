using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        CombatSystem cs = FindAnyObjectByType<CombatSystem>();
        cs.RegisterOnBlock(OnBlock);
        cs.RegisterOnCombatDone(OnCombatDone);

        PlayerController pc = FindAnyObjectByType<PlayerController>();
        pc.RegisterOnStartCombat(OnStartCombat);
    }

    private void OnBlock()
    {
        if (animator != null)
        {
            animator.SetTrigger("Block");
        }
    }

    private void OnCombatDone()
    {
        if (animator != null)
        {
            animator.SetTrigger("EndCombat");
        }
    }

    private void OnStartCombat(Combatant c)
    {
        if (animator != null)
        {
            animator.SetTrigger("StartCombat");
        }
    }

}
