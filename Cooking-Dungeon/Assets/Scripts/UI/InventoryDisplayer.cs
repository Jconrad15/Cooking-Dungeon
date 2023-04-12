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
    private EatingController eatingController;

    private List<GameObject> createdGOs;

    private bool isVisible;

    private void Start()
    {
        createdGOs = new List<GameObject>();
        inventory = FindAnyObjectByType<Inventory>();
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        eatingController = pc.GetComponent<EatingController>();
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

        if (inventoryObjectsArea.TryGetComponent(out Fade fade))
        {
            fade.FadeIn();
        }
    }

    public void HideInventory()
    {
        isVisible = false;

        CleanUI();
        if (inventoryObjectsArea.TryGetComponent(out Fade fade))
        {
            fade.FadeOut();
        }
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
        if (eatingController.TryEatIngredient(ingredientData) == false)
        {
            return;
        }

        createdGOs.Remove(ingredientUI.gameObject);
        Destroy(ingredientUI.gameObject);
    }

    public void EatMealButton(
        MealUI mealUI, MealData mealData)
    {
        if (eatingController.TryEatMeal(mealData) == false)
        {
            return;
        }

        createdGOs.Remove(mealUI.gameObject);
        Destroy(mealUI.gameObject);
    }

}
