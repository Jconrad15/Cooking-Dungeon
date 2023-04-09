using UnityEngine;

/// <summary>
/// The door opener is attached to the player to open doors.
/// </summary>
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
        if (other.gameObject.TryGetComponent(out OpenDoorZone odz))
        {
            TryOpenDoor(odz);
        }
    }

    private void TryOpenDoor(OpenDoorZone odz)
    {
        IngredientData[] neededIngredients =
            new IngredientData[1] { odz.neededIngredient };

        if (inventory.CheckForIngredients(neededIngredients))
        {
            odz.OpenDoor();
        }
    }
}
