using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    food,
    water,
    wood
}

[CreateAssetMenu(fileName = "Resource", menuName = "Inventory/Items/Resource")]
public class ResourceObject : ItemObject
{
    public ResourceType resourceType;

    public void Awake()
    {
        itemType = ItemType.resource;
    }
}
