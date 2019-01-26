using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        inventory.AddItem(item);
        Destroy(item.gameObject);
        Debug.Log("Item collected");
        
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
