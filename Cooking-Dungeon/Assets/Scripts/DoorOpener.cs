using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.TryGetComponent(out OpenDoorZone odz))
        {
            odz.OpenDoor();
        }
    }
}
