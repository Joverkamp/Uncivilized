using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageUI : MonoBehaviour
{
    //ref to storage singleton
    private Storage _storage;

    //components
    public Transform checklist;
    public GameObject tmpPrefab;

    private void Awake()
    {
        //get ref to singleton
        _storage = Storage.instance;

        //subscribe to delegate
        _storage.onStorageChangedCallback += UpdateUI;
    }

    void Start()
    {

    }
    
    void UpdateUI()
    {
        //remove all tmp objects
        foreach(Transform child in checklist.transform)
        {
            Destroy(child.gameObject);
        }

        //make tmp objects for required items
        foreach(StorageSlot requiredSlot in _storage.requiredItems)
        {
            //values for stored v required items
            int stored = 0;
            int required = requiredSlot.amount;

            //get value for stored v required items
            foreach (StorageSlot storedSlot in _storage.storedItems)
            {
                if (requiredSlot.itemType == storedSlot.itemType)
                {
                    //limit stored item display to required amount
                    if(storedSlot.amount > required)
                    {
                        stored = required;
                    }
                    else
                    {
                        stored = storedSlot.amount;
                    }
                    break;
                }
            }

            //create object and set parent to checklist ui
            var newChecklistItem = Instantiate(tmpPrefab);
            newChecklistItem.transform.SetParent(checklist.transform);

            //get TMP component and change text
            TextMeshProUGUI tmp = newChecklistItem.GetComponent<TextMeshProUGUI>();
            tmp.text = requiredSlot.itemType + "(" + stored + "/" + required + ")";
            
            //green text if all items stored
            if(stored >= required)
            {
                tmp.color = Color.green;
            }
            else
            {
                tmp.color = Color.white;
            }

        }
    }
}