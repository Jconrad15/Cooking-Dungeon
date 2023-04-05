using System.Collections;
using System.Collections.Generic;
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

    private void AddIngredient(IngredientData ingredient)
    {
        ingredients.Add(ingredient);
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

    public bool TryRemoveIngredientByCooking(IngredientData ingredient)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].name == ingredient.name)
            {
                ingredients.Remove(ingredients[i]);
                return true;
            }
        }

        return false;
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

    public bool TryGiveMeal(MealData meal)
    {
        for (int i = 0; i < meals.Count; i++)
        {
            if (meals[i].name == meal.name)
            {
                meals.Remove(meals[i]);
                return true;
            }
        }

        return false;
    }

    public bool CheckForIngredients(IngredientData[] neededIngredients)
    {
        bool[] checks = new bool[neededIngredients.Length];

        for (int i = 0; i < neededIngredients.Length; i++)
        {
            for (int j = 0; j < ingredients.Count; j++)
            {
                if (ingredients[j].name == neededIngredients[i].name)
                {
                    //ingredients.Remove(ingredients[j]);
                    checks[i] = true;
                    break;
                }
            }
        }

        // Evaluate if any checks were false
        for (int i = 0; i < checks.Length; i++)
        {
            if (checks[i] == false)
            {
                return false;
            }
        }

        return true;
    }

}
