using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public PrefabLibrary library;

    public ItemType type;
    public ItemName name;
    public ItemStatus status = ItemStatus.Inactive;
    public int actionCost;
    
    public List<Item> canCombineWidth = new List<Item>();

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


