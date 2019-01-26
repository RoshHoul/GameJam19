using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemType type;
    public ItemStatus status = ItemStatus.Inactive;
    public int actionCost;

    public Sprite icon;

    public GameObject inventoryItemPrefab;
    public GameObject worldItemPrefab;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        if(status == ItemStatus.Inactive)
        {
            animator.SetBool("isMoving", true);
            actionCost = 0;
            status = ItemStatus.Activated;
        }
    }
}

public enum ItemType
{
    Collectible,
    Animated
}
public enum ItemStatus
{
    Inactive, //Nothing happened to it yet
    Activated, //It has been picked up / triggered the animation
    Placed, //Placed from inventory
    PlacedConfirmed //Confirmed position
}
