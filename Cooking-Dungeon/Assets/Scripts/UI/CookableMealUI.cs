using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookableMealUI : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private MealData mealData;
    [SerializeField]
    private GameObject ingredientImagePrefab;
    [SerializeField]
    private GameObject ingredientsContainer;

    private void Start()
    {
        image.sprite = mealData.image;
        title.SetText(mealData.name);
        description.SetText(mealData.description);

        CreateIngredientImages();
    }

    public void TryCookMealButton()
    {
        CookingDisplayer cookingDisplayer =
            FindAnyObjectByType<CookingDisplayer>();
        cookingDisplayer.TryCookMealButton(mealData);
    }

    private void CreateIngredientImages()
    {
        for (int i = 0; i < mealData.requiredIngredients.Length; i++)
        {
            GameObject newIngredientImage = Instantiate(
                ingredientImagePrefab, 
                ingredientsContainer.transform);
            Image image = newIngredientImage.GetComponent<Image>();
            image.sprite = mealData.requiredIngredients[i].image;
        }
    }

}
