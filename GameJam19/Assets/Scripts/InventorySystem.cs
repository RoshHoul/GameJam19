using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    List<Item> items = new List<Item>();


    public void AddItem(Item item)
    {
        Debug.Log("Item added");
        items.Add(item);
    }

}
