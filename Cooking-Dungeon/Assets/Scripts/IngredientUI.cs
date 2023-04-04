using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientUI : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

    private IngredientData ingredientData;

    public void Init(IngredientData newIngredientData)
    {
        ingredientData = newIngredientData;
        image.sprite = newIngredientData.image;
        title.SetText(newIngredientData.name);
        description.SetText(newIngredientData.description);
    }

    public void EatIngredientButton()
    {
        InventoryDisplayer inventoryDisplayer =
            FindAnyObjectByType<InventoryDisplayer>();
        inventoryDisplayer.EatIngredientButton(this, ingredientData);
    }
}
