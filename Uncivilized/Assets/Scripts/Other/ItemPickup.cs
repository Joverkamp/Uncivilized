using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemObject item;
    private PlayerInventory _playerInventory;

    public void Start()
    {
        _playerInventory = PlayerInventory.instance;
    }

    public override void OnHoverEnter()
    {
        //TODO create UI billboard prompting player to interact
        //Debug.Log("Started looking at " + item.itemType);
    }

    public override void OnHoverExit()
    {
        //TODO remove UI billboard
        //Debug.Log("Looked away from " + item.itemType);
    }

    public override void OnInteract()
    {
        //Debug.Log("Interacted with " + item.itemType);
        //add item to inventory
        if(_playerInventory.Add(item) == true)
        {
            Destroy(gameObject);
        }

    }
}
