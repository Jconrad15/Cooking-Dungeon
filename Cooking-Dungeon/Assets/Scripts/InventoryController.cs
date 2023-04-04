using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private KeyCode inventoryToggleButton = KeyCode.I;

    private InventoryDisplayer inventoryDisplayer;

    private void Start()
    {
        inventoryDisplayer = FindAnyObjectByType<InventoryDisplayer>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(inventoryToggleButton))
        {
            inventoryDisplayer.Toggle();
        }
    }

    public void ShowInventoryUI()
    {
        inventoryDisplayer.ShowInventory();
    }

    public void HideInventoryUI()
    {
        inventoryDisplayer.HideInventory();
    }
}
