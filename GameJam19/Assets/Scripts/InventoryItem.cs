using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public ItemName name;
    public ItemStatus status = ItemStatus.Activated;
    
    public InventorySystem inventorySystem;

    private Combinator combinator;

    void Start()
    {
        combinator = inventorySystem.combinator;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !combinator.gameObject.activeSelf) // place
        {
            switch (status)
            {
                case ItemStatus.Activated:
                    inventorySystem.PlaceItem(this);
                    Destroy(this.gameObject);
                    break;
            }
            
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {

            switch (status)
            {
                case ItemStatus.Activated:
                    if (!combinator.gameObject.activeSelf)
                    {
                        combinator.gameObject.SetActive(true);
                    }
                    combinator.AddItem(this);
                    break;
                case ItemStatus.CombiningLeft:
                    //give it back to inventory
                    combinator.RemoveItem(this);
                    inventorySystem.AddItem(this);
                    break;
            }
           
        }
    }
    
    public void HighLight()
    {
        GetComponent<Image>().color = Color.yellow;
    }

    public void UnHighLight()
    {
        GetComponent<Image>().color = Color.white;
    }
}
