using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    private KeyCode switchButton = KeyCode.Space;

    [SerializeField]
    private GameObject safeWorld;
    [SerializeField]
    private GameObject dangerWorld;

    private bool isOnSurface;
    private float surfaceOffset = 0.5f;
    private float dungeonOffset = -0.5f;
    public float currentOffset;

    private void Start()
    {
        // This is currently starting the game in the safe world
        isOnSurface = true;
        currentOffset = surfaceOffset;
    }

    private void Update()
    {
        CheckUserInput();
    }

    private void CheckUserInput()
    {
        if (Input.GetKeyDown(switchButton))
        {
            SwitchWorlds();
        }
    }

    private void SwitchWorlds()
    {
        bool canSwitch = CanSwitchHere();
        if (canSwitch == false)
        {
            Debug.Log("Cannot switch here");
            return;
        }

        FlipCharacter();

        // Switch boolean
        isOnSurface = !isOnSurface;
    }

    private void FlipCharacter()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180f);
        if (isOnSurface)
        {
            currentOffset = dungeonOffset;
        }
        else
        {
            currentOffset = surfaceOffset;
        }
    }

    private bool CanSwitchHere()
    {
        Vector3 currentLocation = transform.position;
        currentLocation.y += currentOffset;
        float radius = 0.2f;

        int maxColliders = 2;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(
            currentLocation, radius, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            // Check if overlaping collider 
            hitColliders[i].TryGetComponent(out NoSwitchZone noSwitchZone);
            if (noSwitchZone != null)
            {
                // A no switch zone is present
                return false;
            }
        }

        return true;
    }

}
