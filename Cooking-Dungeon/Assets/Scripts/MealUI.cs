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

    public void Init(MealData meal)
    {
        image.sprite = meal.image;
        title.SetText(meal.name);
        description.SetText(meal.description);
    }


}
