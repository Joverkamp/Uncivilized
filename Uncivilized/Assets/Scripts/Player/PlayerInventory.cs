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

    public List<ItemObject> itemSlots = new List<ItemObject>();
    public int numSlots = 3;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Start()
    {
        for(int i = 0; i < numSlots; i++)
        {
            itemSlots.Add(null);
        }
    }

    public bool Add(ItemObject newItem)
    {
        //look for an empty slot
        for(int i = 0; i < numSlots; i++)
        {
            if(itemSlots[i] == null)
            {
                itemSlots[i] = newItem;

                //update UI
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
                return true;
            }
        }
        //inventory full
        return false;
    }
    public void Drop(Transform dropPos)
    {
        //drop first item in inventory
        for (int i = 0; i < numSlots; i++)
        {
            if (itemSlots[i] != null)
            {
                Instantiate(itemSlots[i].prefab, dropPos.position + dropPos.TransformDirection(new Vector3(0, 1.0f, 2.0f)), transform.rotation);
                itemSlots[i] = null;

                //update UI when an item is dropped
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
                break;
            }
        }
    }

    public void Remove(int index)
    {
        itemSlots[index] = null;

        //update UI when an item is dropped
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}

