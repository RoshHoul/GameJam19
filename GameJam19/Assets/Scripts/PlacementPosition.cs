using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPosition : MonoBehaviour
{
    Item itemBeingHeld = null;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            InventorySystem inventory = player.GetComponent<InventorySystem>();
            if(inventory.isPlacingItem && itemBeingHeld == null)
            {
                itemBeingHeld = player.activeHandItem;
                player.activeHandItem.transform.parent = this.transform;
                player.activeHandItem.transform.position = this.transform.position;
                itemBeingHeld.status = ItemStatus.Placed;
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
                if (itemBeingHeld == player.activeHandItem)
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
