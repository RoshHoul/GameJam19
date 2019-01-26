using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventory;

    bool isInventoryEnabled;

    List<Item> items = new List<Item>();
    
    private void Start()
    {
        isInventoryEnabled = inventory.activeSelf;
    }

    public void ToggleInventory()
    {
        isInventoryEnabled = !isInventoryEnabled;
        inventory.SetActive(isInventoryEnabled);
    }

    public void AddItem(Item item)
    {
        Debug.Log("Item added");
        items.Add(item);
    }

}
