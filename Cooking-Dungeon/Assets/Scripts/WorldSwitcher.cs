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

}
