using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject storyEndScreensPrefab;

    private Inventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<PlayerController>()
            .GetComponent<Inventory>();
    }

    [SerializeField]
    private IngredientData[] lastIngredient;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerEnd();
        }

        if (inventory.CheckForIngredients(lastIngredient))
        {
            TriggerEnd();
        }

    }

    public void TriggerEnd()
    {
        FindAnyObjectByType<PlayerController>().DisableMovement();
        GameObject endScreens = Instantiate(
            storyEndScreensPrefab, canvas.transform);
        endScreens.GetComponent<StoryEndScreenManager>().Init();
    }

}
