using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject worldPrefab;
    public InventorySystem inventorySystem;

    public void Clicked()
    {
        inventorySystem.PlaceItem(this);
        Destroy(this.gameObject);
    }
}
