using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryObjectsArea;
    [SerializeField]
    private GameObject ingredientObjects;
    [SerializeField]
    private GameObject mealObjects;

    [SerializeField]
    private GameObject ingredientPrefab;
    [SerializeField]
    private GameObject mealPrefab;

    private Inventory inventory;
    private Health playerHealth;
    private Combatant playerCombatant;

    private List<GameObject> createdGOs;

    private bool isVisible;

    private void Start()
    {
        createdGOs = new List<GameObject>();
        inventory = FindAnyObjectByType<Inventory>();
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        playerHealth = pc.GetComponent<Health>();
        playerCombatant = pc.GetComponent<Combatant>();
        HideInventory();
    }

    public void Toggle()
    {
        if (isVisible)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }

    public void ShowInventory()
    {
        isVisible = true;
        CleanUI();
        CreateIngredients();
        CreateMeals();

        inventoryObjectsArea.SetActive(true);
    }

    public void HideInventory()
    {
        isVisible = false;

        CleanUI();
        inventoryObjectsArea.SetActive(false);
    }

    private void CreateIngredients()
    {
        List<IngredientData> ingredients = inventory.ingredients;

        for (int i = 0; i < ingredients.Count; i++)
        {
            GameObject newIngredient = Instantiate(
                ingredientPrefab, ingredientObjects.transform);
            createdGOs.Add(newIngredient);
            IngredientUI ingredientUI =
                newIngredient.GetComponent<IngredientUI>();
            ingredientUI.Init(ingredients[i]);
        }
    }

    private void CreateMeals()
    {
        List<MealData> meals = inventory.meals;

        for (int i = 0; i < meals.Count; i++)
        {
            GameObject newMeal = Instantiate(
                mealPrefab, mealObjects.transform);
            createdGOs.Add(newMeal);
            MealUI mealUI =
                newMeal.GetComponent<MealUI>();
            mealUI.Init(meals[i]);
        }
    }

    private void CleanUI()
    {
        for (int i = createdGOs.Count - 1; i >= 0; i--)
        {
            Destroy(createdGOs[i]);
        }
    }

    public void EatIngredientButton(
        IngredientUI ingredientUI, IngredientData ingredientData)
    {
        if (inventory.TryRemoveIngredient(ingredientData) == false)
        {
            return;
        }

        // Apply effects of ingredient
        playerHealth.IncreaseMaxHealth(ingredientData.increaseMaxHealthAmount);
        playerHealth.Heal(ingredientData.healAmount);


        createdGOs.Remove(ingredientUI.gameObject);
        Destroy(ingredientUI.gameObject);
    }

    public void EatMealButton(
        MealUI mealUI, MealData mealData)
    {
        if (inventory.TryRemoveMeal(mealData) == false)
        {
            return;
        }

        // Apply effects of meal
        playerCombatant.IncreaseDamageDealt(mealData.increaseDamageDealt);
        playerHealth.IncreaseMaxHealth(mealData.increaseMaxHealthAmount);
        playerHealth.Heal(mealData.healAmount);

        createdGOs.Remove(mealUI.gameObject);
        Destroy(mealUI.gameObject);
    }

}
