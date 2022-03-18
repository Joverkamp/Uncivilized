using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    PlayerInventory inventory;
    InventorySlot slot;

 
    void Start()
    {
        inventory = PlayerInventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slot = GetComponentInChildren<InventorySlot>();
    }

    void UpdateUI()
    {

        if(inventory.heldItem == null)
        {
            slot.Remove();
        }
        else
        {
            slot.Add(inventory.heldItem);
        }
    }
}
