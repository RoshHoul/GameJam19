using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    BoxCollider col;
    InventorySystem inventory;
    

    Item currentItem;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        inventory = GetComponent<InventorySystem>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (currentItem != null)
            {
                if(currentItem.type == ItemType.Collectible)
                {
                    CollectItem(currentItem);
                }
                else if (currentItem.type == ItemType.Animated)
                {
                    currentItem.TriggerAnimation();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.ToggleInventory();
        }
    }

    void CollectItem(Item item)
    {
        if(inventory.CanCollectItem())
        {
            inventory.AddItem(item);
        }
        else
        {
            Debug.Log("Inventory full");
            //tell the ui to say "inventory full"
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item anyItem = other.GetComponent<Item>();

        if (anyItem != null)
        {
            currentItem = anyItem;
        }
        else
        {
            currentItem = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item anyItem = other.GetComponent<Item>();

        if (anyItem != null)
        {
            currentItem = null;
        }
    }
    
}
