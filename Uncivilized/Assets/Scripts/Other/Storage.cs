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


    public delegate void OnRequirementsMet();
    public static OnRequirementsMet requirementsMetCallback;

    private void Start()
    {
        //update UI when a new item is stored
        if (onStorageChangedCallback != null)
        {
            onStorageChangedCallback.Invoke();
        }
    }

    public bool AddItem(ItemObject item)
    {
        for (int i = 0; i < storedItems.Count; i++)
        {
            if (storedItems[i].itemType == item.itemType)
            {
                //add existing item to storage inventory

                //increment value
                storedItems[i].Increment();

                //update UI when a new item is stored
                if (onStorageChangedCallback != null)
                {
                    onStorageChangedCallback.Invoke();
                }
                CheckRequirementsMet();
                return true;
            }
        }
        //add new item to storage inventory

        //add item to dictionary
        storedItems.Add(new StorageSlot(item.itemType, 1));

        //update UI when a new item is stored
        if (onStorageChangedCallback != null)
        {
            onStorageChangedCallback.Invoke();
        }
        return true;
    }

    void CheckRequirementsMet()
    {
        bool requirementsMet = true;

        //check to see f each storage slot is <= the required amount
        foreach (StorageSlot requiredSlot in requiredItems)
        {
            //get value for stored v required items
            foreach (StorageSlot storedSlot in storedItems)
            {
                if (requiredSlot.itemType == storedSlot.itemType)
                {
                    if(storedSlot.amount < requiredSlot.amount)
                    {
                        requirementsMet = false;
                        break;
                    }
                }
            }
        }

        //invoke event that staorage requirements have been met
        if (requirementsMet)
        {
            if (requirementsMetCallback != null)
            {
                requirementsMetCallback.Invoke();
            }
        }
    }   

    public void AddItemRequirement(ItemType itemType, int amount)
    {
        requiredItems.Add(new StorageSlot(itemType, amount));
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
    public ItemType itemType;
    public int amount;

    public StorageSlot(ItemType _itemType, int _amount)
    {
        itemType = _itemType;
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
