using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorZone : MonoBehaviour
{
    [SerializeField]
    private Door door;

    [SerializeField]
    public IngredientData neededIngredient;

    public void OpenDoor()
    {
        door.OpenDoor();
        Destroy(gameObject);
    }




}
