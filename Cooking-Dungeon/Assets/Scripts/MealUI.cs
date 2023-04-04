using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MealUI : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

    private MealData mealData;

    public void Init(MealData newMealData)
    {
        mealData = newMealData;
        image.sprite = newMealData.image;
        title.SetText(newMealData.name);
        description.SetText(newMealData.description);
    }

    public void EatMealButton()
    {
        InventoryDisplayer inventoryDisplayer =
            FindAnyObjectByType<InventoryDisplayer>();
        inventoryDisplayer.EatMealButton(this, mealData);
    }

}
