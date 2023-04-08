using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorZone : MonoBehaviour
{
    [SerializeField]
    private Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController pc))
        {
            Destroy(door);
            Destroy(gameObject);
        }
    }




}
