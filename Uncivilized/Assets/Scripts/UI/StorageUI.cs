using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageUI : MonoBehaviour
{
    //ref to storage singleton
    Storage storage;

    //components
    public Transform checklist;
    public GameObject tmpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //get ref to singleton
        storage = Storage.instance;

        //subscribe to delegate
        storage.onStorageChangedCallback += UpdateUI;

        //TODO level requirements need to be handled elsewhere
        storage.AddItemRequirement(ResourceType.food, 5);
        storage.AddItemRequirement(ResourceType.water, 6);
        storage.AddItemRequirement(ResourceType.wood, 4);
    }
    
    void UpdateUI()
    {
        //remove all tmp objects
        foreach(Transform child in checklist.transform)
        {
            Destroy(child.gameObject);
        }

        //make tmp objects for required items
        foreach(StorageSlot requiredSlot in storage.requiredItems)
        {
            //values for stored v required items
            int stored = 0;
            int required = requiredSlot.amount;

            //get value for stored v required items
            foreach (StorageSlot storedSlot in storage.storedItems)
            {
                if (requiredSlot.resourceType == storedSlot.resourceType)
                {
                    stored = storedSlot.amount;
                    break;
                }
            }

            //create object and set parent to checklist ui
            var newChecklistItem = Instantiate(tmpPrefab);
            newChecklistItem.transform.SetParent(checklist.transform);

            //get TMP component and change text
            TextMeshProUGUI tmp = newChecklistItem.GetComponent<TextMeshProUGUI>();
            tmp.text = requiredSlot.resourceType + "(" + stored + "/" + required + ")";

        }
    }
}