using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    resource
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public ItemType itemType;
    [TextArea(15,20)]
    public string desciption;

    public virtual void Use()
    {
        Debug.Log("Iteracted with" + itemType);
    }
}