using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;
    public int actionCost;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        animator.SetBool("isMoving", true);
        actionCost = 0;
    }
}

public enum ItemType
{
    Collectible,
    Animated
}
