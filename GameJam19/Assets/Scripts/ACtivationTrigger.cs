using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACtivationTrigger : MonoBehaviour
{
    public PlacementPosition parent;
    bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !hasPlayed && parent.presetTrap.activeSelf)
        {
            parent.Trigger();
            hasPlayed = true;
        }
    }
}
