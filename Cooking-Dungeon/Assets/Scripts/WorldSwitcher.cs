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

    private bool isSafeWorld;

    private void Start()
    {
        // This is currently starting the game in the safe world
        isSafeWorld = true;
        safeWorld.SetActive(true);
        dangerWorld.SetActive(false);
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

        if (isSafeWorld)
        {
            dangerWorld.SetActive(true);
            safeWorld.SetActive(false);
        }
        else
        {
            safeWorld.SetActive(true);
            dangerWorld.SetActive(false);
        }

        // Switch boolean
        isSafeWorld = !isSafeWorld;
    }

    private bool CanSwitchHere()
    {
        Vector3 currentLocation = transform.position;
        currentLocation.y += 0.5f;
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
