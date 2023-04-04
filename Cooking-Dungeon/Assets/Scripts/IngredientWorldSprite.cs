using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientWorldSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    private void Start()
    {
        Ingredient ingredient = GetComponent<Ingredient>();

        sr.sprite = ingredient.ingredientData.image;
    }


}
