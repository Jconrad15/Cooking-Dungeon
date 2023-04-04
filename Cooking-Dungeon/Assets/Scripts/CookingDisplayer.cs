using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingDisplayer : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryController inventoryController;

    [SerializeField]
    private GameObject cookUI;

    private void Start()
    {
        inventoryController = GetComponent<InventoryController>();

        playerController = FindAnyObjectByType<PlayerController>();
        playerController.RegisterOnStartCook(OnStartCook);

        cookUI.SetActive(false);
    }

    private void OnStartCook(CookStation cookStation)
    {
        playerController.DisableMovement();
        ShowCookUI();
        StartCoroutine(Cook());
    }

    private IEnumerator Cook()
    {
        yield return new WaitForSeconds(0.2f);

        // TODO: cooking goes here
        bool done = false;
        while (done == false)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                done = true;
                break;
            }

            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Cooking Done");
        HideCookUI();
        playerController.EnableMovement();
    }

    private void ShowCookUI()
    {
        cookUI.SetActive(true);
        inventoryController.ShowInventoryUI();
    }

    private void HideCookUI()
    {
        cookUI.SetActive(false);
        inventoryController.HideInventoryUI();
    }

    public void TryCookMealButton(MealData mealData)
    {
        Debug.Log("Try to cook the meal");
        // Need to check that the player has the ingredients
        IngredientData[] neededIngredients = mealData.requiredIngredients;
        // Check with inventory
        Inventory inventory = FindAnyObjectByType<Inventory>();
        
        if (inventory.CheckForIngredients(neededIngredients) == true)
        {
            // This can be cooked
            // Add meal and remove ingredients
            inventory.AddMeal(Instantiate(mealData));
            for (int i = 0; i < neededIngredients.Length; i++)
            {
                inventory.TryRemoveIngredientByCooking(neededIngredients[i]);
            }
            Debug.Log("Cooked");
            inventoryController.ShowInventoryUI();
        }
        else
        {
            // This cannot be cooked
            // Do nothing?
            // TODO: indicate to player that this cannot be made
            Debug.Log("Not Cooked");
        }

    }

}
