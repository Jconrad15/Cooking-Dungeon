using System;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    private Animator animator;

    private Action<Combatant> cbOnAttack;

    public int damageDealt = 1;
    public string combatantName;

    public Health health;

    [SerializeField]
    public GameObject droppedIngredientPrefab;

    private void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    public void Attack(Combatant otherCombatant)
    {
        otherCombatant.health.Hurt(damageDealt);
        cbOnAttack?.Invoke(this);

        // Animation attack
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

    }

    public void RegisterOnAttack(Action<Combatant> callbackfunc)
    {
        cbOnAttack += callbackfunc;
    }

    public void UnregisterOnAttack(Action<Combatant> callbackfunc)
    {
        cbOnAttack -= callbackfunc;
    }

}
