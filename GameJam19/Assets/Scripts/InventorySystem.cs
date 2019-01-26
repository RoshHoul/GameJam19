using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InventorySystem : MonoBehaviour
{
    public PrefabLibrary library;
    public GameObject inventoryItemPrefab;

    Player player;

    public GameObject inventoryRoot;
    public GameObject itemsParent;
    public GameObject disabledObjects;
    public Combinator combinator;

    public bool isPlacingItem = false;

    bool isInventoryEnabled;

    public List<InventoryItem> items = new List<InventoryItem>();
    List<Image> images = new List<Image>();

    FirstPersonController fpsController;

    private void Start()
    {
        player = GetComponent<Player>();
        isInventoryEnabled = inventoryRoot.activeSelf;
        fpsController = GetComponent<FirstPersonController>();
    }

    public void ToggleInventory()
    {
        isInventoryEnabled = !isInventoryEnabled;
        inventoryRoot.SetActive(isInventoryEnabled);

        fpsController.enabled = !fpsController.isActiveAndEnabled;
        if (!fpsController.enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
        }
    }

    public void AddItem(ItemName name)
    {
        InventoryItem newItem = Instantiate(inventoryItemPrefab, itemsParent.transform).GetComponent<InventoryItem>();
        newItem.GetComponent<Image>().sprite = library.GetSprite(name);
        newItem.inventorySystem = this;
        newItem.name = name;
        items.Add(newItem);
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        item.status = ItemStatus.Activated;
        item.transform.parent = itemsParent.transform;
    }

    public void RemoveItem(ItemName removeName)
    {
        InventoryItem toRemove = items.First(item => item.name == removeName);
        items.Remove(toRemove);
    }

    public void HighlightItem(ItemName name)
    {
        InventoryItem toLight = items.First(x => x.name == name);
        if(toLight != null)
        {
            toLight.HighLight();
        }
    }

    public void UnHighlightItem(ItemName name)
    {
        InventoryItem toLight = items.First(x => x.name == name);
        if (toLight != null)
        {
            toLight.UnHighLight();
        }
    }

    public void PlaceItem(InventoryItem item)
    {
        isPlacingItem = true;

        player.LoadInHand(item.name);
       
        ToggleInventory();
    }

    public bool CanCollectItem()
    {
        return items.Count < 10;
    }
    
}
