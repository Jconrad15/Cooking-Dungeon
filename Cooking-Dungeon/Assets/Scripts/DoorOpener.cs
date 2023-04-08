using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory =
            FindAnyObjectByType<PlayerController>()
            .GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.TryGetComponent(out OpenDoorZone odz))
        {
            IngredientData[] neededIngredients =
                new IngredientData[1] {odz.neededIngredient};

            if (inventory.CheckForIngredients(neededIngredients))
            {
                odz.OpenDoor();
            }
        }
    }
}
