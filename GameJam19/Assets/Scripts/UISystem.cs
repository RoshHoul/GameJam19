using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    GameObject inventory;

    bool isInventoryEnabled;

    public void ToggleInventory()
    {
        isInventoryEnabled = !isInventoryEnabled;
        inventory.SetActive(isInventoryEnabled);
    }
}
