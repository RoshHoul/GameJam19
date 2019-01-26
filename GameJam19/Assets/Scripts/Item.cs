using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemType type;

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
        animator.SetBool("isMoving", true);
    }
}

public enum ItemType
{
    Collectible,
    Animated
}
