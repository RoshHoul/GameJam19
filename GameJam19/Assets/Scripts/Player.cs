using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    GameManager gameManager;

    BoxCollider col;
    InventorySystem inventory;

    //Item in hand
    public GameObject itemHolder;
    public GameObject activeHandItem;
    
    Item itemInVicinity;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        inventory = GetComponent<InventorySystem>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(inventory.isPlacingItem)
            {
                if(activeHandItem != null && activeHandItem.GetComponent<Item>().status == ItemStatus.Placed)
                {
                    inventory.isPlacingItem = false;
                    activeHandItem.GetComponent<Item>().status = ItemStatus.PlacedConfirmed;
                }
            }
            else
            {
                if (itemInVicinity != null)
                {
                    if (itemInVicinity.type == ItemType.Collectible)
                    {
                        CollectItem(itemInVicinity);
                    }
                    else if (itemInVicinity.type == ItemType.Animated)
                    {
                        gameManager.ApplyDayAction(itemInVicinity.actionCost);
                        itemInVicinity.TriggerAnimation();
                    }
                }
            }
            //gameManager.ApplyDayAction(2); // Quick test on action points
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (activeHandItem != null)
            {
                inventory.AddItem(activeHandItem.GetComponent<Item>());
                //Destroy(activeHandItem);
                inventory.isPlacingItem = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.ToggleInventory();
        }
    }

    void CollectItem(Item item)
    {
        if(inventory.CanCollectItem() && item.status == ItemStatus.Inactive)
        {
            if(item.status == ItemStatus.Inactive)
            {
                inventory.AddItem(item);
                item.status = ItemStatus.Activated;
            }
        }
        else
        {
            //tell the ui to say "inventory full" or it has alreadt been placed
        }
    }

    public void LoadInHand(InventoryItem item)
    {

        activeHandItem = Instantiate(item.worldPrefab, itemHolder.transform, false);

    }

    private void OnTriggerEnter(Collider other)
    {
        Item anyItem = other.GetComponent<Item>();

        if (anyItem != null)
        {
            itemInVicinity = anyItem;
        }
        else
        {
            itemInVicinity = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item anyItem = other.GetComponent<Item>();

        if (anyItem != null)
        {
            itemInVicinity = null;
        }
    }
    
}
