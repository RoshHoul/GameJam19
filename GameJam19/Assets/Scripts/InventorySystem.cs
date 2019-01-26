using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InventorySystem : MonoBehaviour
{
    Player player;

    public GameObject inventoryRoot;
    public GameObject itemsParent;
    public GameObject disabledObjects;

    public bool isPlacingItem = false;

    bool isInventoryEnabled;

    List<InventoryItem> items = new List<InventoryItem>();
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

    public void AddItem(Item item)
    {
        InventoryItem newItem = Instantiate(item.inventoryItemPrefab, itemsParent.transform).GetComponent<InventoryItem>();
        newItem.GetComponent<Image>().sprite = item.icon;
        newItem.worldPrefab = item.worldItemPrefab;
        newItem.inventorySystem = this;
        items.Add(newItem);

        item.gameObject.transform.parent = disabledObjects.transform;
        item.gameObject.transform.position = disabledObjects.transform.position;
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
    }

    public void PlaceItem(InventoryItem item)
    {
        isPlacingItem = true;

        player.LoadInHand(item);
       
        ToggleInventory();
    }

    public bool CanCollectItem()
    {
        return items.Count < 10;
    }

}
