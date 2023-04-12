using UnityEngine;

public class EatingController : MonoBehaviour
{
    private Inventory inventory;
    private Health playerHealth;
    private Combatant playerCombatant;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        playerHealth = pc.GetComponent<Health>();
        playerCombatant = pc.GetComponent<Combatant>();
    }

    public bool TryEatIngredient(IngredientData ingredientData)
    {
        if (inventory.TryRemoveIngredient(ingredientData) == false)
        {
            return false;
        }

        ApplyIngredientEffects(ingredientData);
        return true;
    }

    public bool TryEatMeal(MealData mealData)
    {
        if (inventory.TryRemoveMeal(mealData) == false)
        {
            return false;
        }

        ApplyMealEffects(mealData);

        return true;
    }

    private void ApplyIngredientEffects(IngredientData ingredientData)
    {
        playerHealth.IncreaseMaxHealth(
            ingredientData.increaseMaxHealthAmount);
        playerHealth.Heal(
            ingredientData.healAmount);
    }

    private void ApplyMealEffects(MealData mealData)
    {
        playerCombatant.IncreaseDamageDealt(
            mealData.increaseDamageDealt);
        playerHealth.IncreaseMaxHealth(
            mealData.increaseMaxHealthAmount);
        playerHealth.Heal(
            mealData.healAmount);
    }
}
