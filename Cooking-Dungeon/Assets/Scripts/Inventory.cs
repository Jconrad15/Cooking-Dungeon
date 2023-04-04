using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<IngredientData> ingredients;
    public List<MealData> meals;

    private void Start()
    {
        ingredients = new List<IngredientData>();
        meals = new List<MealData>();

        PlayerController pc = FindAnyObjectByType<PlayerController>();
        pc.RegisterOnRunIntoItem(OnRunIntoItem);

    }

    private void OnRunIntoItem(Ingredient newIngredient)
    {
        // Create clone of ingredient data
        var clone = Instantiate(newIngredient.ingredientData);

        AddIngredient(clone);
        Destroy(newIngredient.gameObject);
    }

    public void AddIngredient(IngredientData ingredient)
    {
        ingredients.Add(ingredient);
        Destroy(ingredient);
    }

    public bool TryRemoveIngredient(IngredientData ingredient)
    {
        if (ingredients.Contains(ingredient) == false)
        {
            return false;
        }

        ingredients.Remove(ingredient);
        return true;
    }

    public void AddMeal(MealData meal)
    {
        meals.Add(meal);
    }

    public bool TryRemoveMeal(MealData meal)
    {
        if (meals.Contains(meal) == false)
        {
            return false;
        }

        meals.Remove(meal);
        return true;
    }


}
