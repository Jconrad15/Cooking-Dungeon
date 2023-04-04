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

    public void Init(IngredientData ingredient)
    {
        image.sprite = ingredient.image;
        title.SetText(ingredient.name);
        description.SetText(ingredient.description);
    }
}
