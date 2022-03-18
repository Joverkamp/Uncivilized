using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    food,
    water,
    wood,
    experience
}

[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public ItemType itemType;
    [TextArea(15,20)]
    public string desciption;

    public void Use()
    {
        Debug.Log("Iteracted with" + itemType);
    }
}