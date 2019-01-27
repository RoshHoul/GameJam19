using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public PrefabLibrary library;
    GameManager gameManager;

    BoxCollider col;
    InventorySystem inventory;
    FirstPersonController fpsController;

    //Item in hand
    public GameObject itemHolder;
    public Item activeHandItem;
    public GameObject cameraBrowserObj;
    Item itemInVicinity;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        inventory = GetComponent<InventorySystem>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        fpsController = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(inventory.isPlacingItem)
            {
                if(activeHandItem != null && activeHandItem.GetComponent<Item>().status == ItemStatus.Placed)
                {
                    //inventory.isPlacingItem = false;
                    //inventory.RemoveItem(activeHandItem.name);
                    //activeHandItem.GetComponent<Item>().status = ItemStatus.PlacedConfirmed;
                    //Destroy(activeHandItem.gameObject);
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
                inventory.AddItem(activeHandItem.name);
                inventory.isPlacingItem = false;
                Destroy(activeHandItem.gameObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            cameraBrowserObj.SetActive(!cameraBrowserObj.activeSelf);
            if (cameraBrowserObj.activeSelf)
            {
                fpsController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = !Cursor.visible;
            }
            else
            {
                fpsController.enabled = true;
            }
        }
    }

    void CollectItem(Item item)
    {
        if(inventory.CanCollectItem())
        {
            if(item.status == ItemStatus.Inactive)
            {
                inventory.AddItem(item.name);
                Destroy(item.gameObject);
            }
        }
        else
        {
            //tell the ui to say "inventory full" or it has alreadt been placed
        }
    }

    public void LoadInHand(ItemName name)
    {
        activeHandItem = Instantiate(library.GetPrefab(name), itemHolder.transform, false).GetComponent<Item>();
        activeHandItem.transform.position = itemHolder.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemyAgent>() != null)
        {
            FindObjectOfType<GameManager>().GameOver(false);
        }

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
