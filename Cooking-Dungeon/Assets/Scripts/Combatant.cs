using System;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    private Animator animator;

    private Action<Combatant> cbOnAttack;

    public int damageDealt = 1;
    public string combatantName;

    public Health health;

    [SerializeField]
    private GameObject droppedIngredientPrefab;

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

    public void DropIngredient()
    {
        if (droppedIngredientPrefab != null)
        {
            GameObject ingredient = Instantiate(
                droppedIngredientPrefab);
            ingredient.transform.position = transform.position;

            FaceCamera iFaceCamera =
                ingredient.GetComponent<FaceCamera>();
            FaceCamera combatantFaceCamera =
                GetComponent<FaceCamera>();
            iFaceCamera.SetIsOnSurface(
                combatantFaceCamera.CheckIsOnSurface());
        }
    }

    public void IncreaseDamageDealt(int increaseAmount)
    {
        damageDealt += increaseAmount;
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
