using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Meal : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;
    public Ingredient[] requiredIngredients;


}
