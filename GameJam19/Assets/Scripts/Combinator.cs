using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combinator : MonoBehaviour
{
    public PrefabLibrary library;
    public List<InventoryItem> items;

    private InventoryItem createdObject;

    public InventorySystem inventory;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject cancelButton;
    public GameObject confirmButton;
    
    public void AddItem(InventoryItem item)
    {
        item.UnHighLight();
        item.transform.parent = leftSide.transform;
        items.Add(item);

        item.status = ItemStatus.CombiningLeft;

        inventory.RemoveItem(item.name);

        HighlightItems(item.name);

        CheckCombination();
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        CheckCombination();
    }

    public void CreateItem(ItemName name)
    {
        InventoryItem newItem = Instantiate(inventory.inventoryItemPrefab, rightSide.transform).GetComponent<InventoryItem>();
        newItem.GetComponent<Image>().sprite = library.GetSprite(name);
        newItem.inventorySystem = inventory;
        newItem.name = name;
        
        newItem.status = ItemStatus.CombiningRight;
        createdObject = newItem;

        confirmButton.SetActive(true);
    }

    public void HighlightItems(ItemName itemName)
    {
        List<ItemName> itemsToHighlight = library.GetCombinationsContaining(itemName);
        foreach (var item in itemsToHighlight)
        {
            if(!IsInList(item,items))
            {
                inventory.HighlightItem(item);
            }
        }
    }

    public void CheckCombination()
    {
        List<ItemName> itemNames = new List<ItemName>();
        foreach (var item in items)
        {
            itemNames.Add(item.name);   
        }
        ItemName result = library.FindCombinationResult(itemNames);

        if(result != ItemName.Unset)
        {
            CreateItem(result);
        }
    }

    bool IsInList(ItemName name, List<InventoryItem> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].name == name)
            {
                return true;
            }
        }

        return false;
    }

    public void Confirm()
    {
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }

        items.Clear();

        inventory.AddItem(createdObject.name);

        confirmButton.SetActive(false);

        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        foreach (var item in items)
        {
            inventory.AddItem(item);
        }

        if(createdObject != null)
        {
            Destroy(createdObject.gameObject);
        }
       
        items.Clear();

        confirmButton.SetActive(false);

        gameObject.SetActive(false);
    }
}
