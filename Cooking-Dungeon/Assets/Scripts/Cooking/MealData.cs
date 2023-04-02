using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MealData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;
    public IngredientData[] requiredIngredients;


}
