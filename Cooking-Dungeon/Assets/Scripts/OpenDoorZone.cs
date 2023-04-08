using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorZone : MonoBehaviour
{
    [SerializeField]
    private Door door;

    public void OpenDoor()
    {
        door.OpenDoor();
        Destroy(gameObject);
    }




}
