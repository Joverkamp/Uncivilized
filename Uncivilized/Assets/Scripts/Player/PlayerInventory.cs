using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    #region Singleton
    public static PlayerInventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public ItemObject heldItem;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(ItemObject item)
    {
        //already holding an item
        if(heldItem != null)
        {
            //TODO: display UI message
            Debug.Log("Already holding an item");
            return false;
        }
        //set new held item
        else
        {
            heldItem = item;
            //update UI when a new item is picked up
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            return true;
        }
    }
    public void Drop(Transform dropPos)
    {
        if (heldItem != null)
        {
            //drop item in world space in front of player
            Instantiate(heldItem.prefab, dropPos.position + dropPos.TransformDirection(new Vector3(0, 1.0f, 2.0f)), transform.rotation);
            heldItem = null;

            //update UI when an item is dropped
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        else
        {
            //TODO: display UI message
            Debug.Log("Not holding an item");
        }
    }

    public void Remove()
    {
        heldItem = null;

        //update UI when an item is dropped
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
