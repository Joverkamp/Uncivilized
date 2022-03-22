using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : Interactable
{
    private PlayerInventory _playerInventory;
    private Storage _storage;

    private void Start()
    {
        _playerInventory = PlayerInventory.instance;
        _storage = Storage.instance;
    }

    public override void OnHoverEnter()
    {
        //TODO create UI billboard prompting player to store
        //Debug.Log("Started looking at storage");
    }

    public override void OnHoverExit()
    {
        //TODO remove UI billboard
        //Debug.Log("Looked away from storage");
    }

    public override void OnInteract()
    {
        //Debug.Log("Interacted with storage");
        //add item to storage and remove from inventory
        for(int i = 0; i < _playerInventory.numSlots; i++)
        {
            if(_playerInventory.itemSlots[i] != null)
            {
                //remove item from inventory only if storage successful
                if (_storage.AddItem(_playerInventory.itemSlots[i]))
                {
                    _playerInventory.Remove(i);
                }
                break;
            }
        }
    }
}
