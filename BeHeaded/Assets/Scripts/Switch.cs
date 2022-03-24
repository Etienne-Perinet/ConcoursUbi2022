using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : Interactable
{
    
    [SerializeField] protected bool isOn = false;

    public abstract void UpdateObject();

    public override bool GetState() => isOn;

    public override void Interact(Vector3 position)
    {
        if(!IsBroken)
        {
            isOn = !isOn;
            UpdateObject();
        }
    }

    public override void Lock(Vector3 position)
    {
        if(!IsLocked) ToggleLock();
    }

    public override void Unlock(Vector3 position) 
    {
        if(IsLocked) ToggleLock();
    } 
}
