using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //ref to inventory singleton
    private PlayerInventory _playerInventory;

    //components
    public Transform parent;
    public GameObject inventorySlotPrefab;
    public List<GameObject> uiInventorySlots = new List<GameObject>();
    public Sprite emptyIcon;

    void Start()
    {
        //get ref to singleton
        _playerInventory = PlayerInventory.instance;

        //subscribe to delegate
        _playerInventory.onItemChangedCallback += UpdateUI;
        
        //set up ui inventory slots
        for(int i = 0; i < _playerInventory.numSlots; i++)
        {
            var newInventorySlot = Instantiate(inventorySlotPrefab);
            uiInventorySlots.Add(newInventorySlot);
            newInventorySlot.transform.SetParent(parent.transform);
        }
    }

    void UpdateUI()
    {
        for(int i = 0;i < _playerInventory.numSlots; i++)
        {
            //inventory slot item
            ItemObject item = _playerInventory.itemSlots[i];

            //gameobject to display sprite
            Transform spriteSlot = uiInventorySlots[i].transform.GetChild(0);

            //if item in inventory slot is not empty display sprite
            if (item != null)
            {
                spriteSlot.GetComponent<Image>().sprite = item.icon;
            }
            else
            {
                spriteSlot.gameObject.GetComponent<Image>().sprite = emptyIcon;
            }
        }
    }
}
