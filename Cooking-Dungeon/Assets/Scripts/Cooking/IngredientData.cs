using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{ 
    public new string name;
    public string description;
    public Sprite image;

    public int healAmount;
    public int increaseMaxHealthAmount;
}
