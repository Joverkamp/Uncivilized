using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    //ref to child image component
    public Image icon;

    public void Add(ItemObject item)
    {
        //change image to 
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void Remove()
    {
        //disable image to hide sprite
        icon.enabled = false;
    }
}
