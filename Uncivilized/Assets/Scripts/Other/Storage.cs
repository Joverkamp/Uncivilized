using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Storage : MonoBehaviour
{
    #region Singleton
    public static Storage instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of storage inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public List<StorageSlot> storedItems = new List<StorageSlot>();
    public List<StorageSlot> requiredItems = new List<StorageSlot>();

    public delegate void OnStorageChanged();
    public OnStorageChanged onStorageChangedCallback;

    public bool AddItem(ItemObject item)
    {
        //check that item is a resource object
        if (item.GetType() == typeof(ResourceObject))
        {
            ResourceObject resourceItem = (ResourceObject)item;

            for (int i = 0; i < storedItems.Count; i++)
            {
                if (storedItems[i].resourceType == resourceItem.resourceType)
                {
                    //add existing item to storage inventory
                    Debug.Log("Item incremented in storage");

                    //increment value
                    storedItems[i].Increment();

                    //update UI when a new item is stored
                    if (onStorageChangedCallback != null)
                    {
                        onStorageChangedCallback.Invoke();
                    }
                    return true;
                }
            }
            //add new item to storage inventory
            Debug.Log("New item added to storage");

            //add item to dictionary
            storedItems.Add(new StorageSlot(resourceItem.resourceType, 1));

            //update UI when a new item is stored
            if (onStorageChangedCallback != null)
            {
                onStorageChangedCallback.Invoke();
            }
            return true;

        }
        //item is not a resource object
        else
        {
            Debug.Log("Could not add item to storage");
            return false;
        }
    }

    public void AddItemRequirement(ResourceType resourceType, int amount)
    {
        requiredItems.Add(new StorageSlot(resourceType, amount));
        if (onStorageChangedCallback != null)
        {
            onStorageChangedCallback.Invoke();
        }
    }

    public void Clear()
    {

    }
}

[System.Serializable]
public class StorageSlot
{
    public ResourceType resourceType;
    public int amount;

    public StorageSlot(ResourceType _resourceType, int _amount)
    {
        resourceType = _resourceType;
        amount = _amount;
    }

    public void Increment()
    {
        amount += 1;
    }

    public void SetAmount(int _amount)
    {
        amount = _amount;
    }
}
