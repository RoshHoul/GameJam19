using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPosition : MonoBehaviour
{
    public List<ItemName> canTakeItems = new List<ItemName>();
    public GameObject presetTrap;

    Item itemBeingHeld = null;

    public Text sign;

    public void Awake()
    {
        presetTrap.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (itemBeingHeld != null && itemBeingHeld.status == ItemStatus.Placed)
            {
                Player player = FindObjectOfType<Player>();

                FindObjectOfType<GameManager>().ApplyDayAction(player.activeHandItem.actionCost);
                Destroy(player.activeHandItem);
                sign.enabled = false;
            }
        }
    }

    public void Trigger()
    {
        if(presetTrap.activeSelf)
        {
            presetTrap.GetComponent<Animator>().SetTrigger("play");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            InventorySystem inventory = player.GetComponent<InventorySystem>();
            if(inventory.isPlacingItem && itemBeingHeld == null && canTakeItems.Contains(player.activeHandItem.name))
            {
                if(presetTrap != null)
                {
                    //Destroy(player.activeHandItem);
                    player.activeHandItem.gameObject.SetActive(false);
                    presetTrap.SetActive(true);
                }
                else
                {
                    itemBeingHeld = player.activeHandItem;
                    player.activeHandItem.transform.parent = this.transform;
                    player.activeHandItem.transform.position = this.transform.position;
                    itemBeingHeld.status = ItemStatus.Placed;
                }
                

                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(itemBeingHeld != null && player != null)
        {
            InventorySystem inventory = player.GetComponent<InventorySystem>();
            if (inventory.isPlacingItem)
            {
                if (itemBeingHeld.name == player.activeHandItem.name)
                {
                    if(presetTrap != null)
                    {
                        player.activeHandItem.gameObject.SetActive(true);
                        presetTrap.SetActive(false);
                        presetTrap.GetComponent<Item>().status = ItemStatus.Activated;
                    }
                    else
                    {
                        itemBeingHeld.transform.parent = player.itemHolder.transform;
                        itemBeingHeld.transform.position = player.itemHolder.transform.position;
                        itemBeingHeld.status = ItemStatus.Activated;
                        itemBeingHeld = null;
                    }
                }
            }
        }
    }
}
