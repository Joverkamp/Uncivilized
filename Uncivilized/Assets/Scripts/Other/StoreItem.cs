using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : Interactable
{
    public override void OnHoverEnter()
    {
        //TODO create UI billboard prompting player to store
        Debug.Log("Started looking at storage");
    }

    public override void OnHoverExit()
    {
        //TODO remove UI billboard
        Debug.Log("Looked away from storage");
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with storage");
        //add item to storage and remove from inventory
        if(PlayerInventory.instance.heldItem != null)
        {
            //remove item from inventory only if storage successful
            if (Storage.instance.AddItem(PlayerInventory.instance.heldItem))
            {
                PlayerInventory.instance.Remove();
            }
        }
    }
}
