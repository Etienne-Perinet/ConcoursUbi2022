using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : Interactable
{
    [SerializeField] protected float autoUnlockTime = 5f;
    [SerializeField] protected bool autoUnlock;
    

    // protected Transform playerBody;
    public bool isOpen = false;
    protected float timer = 0f; 

    public abstract void ToggleDoor(Vector3 position);

    public virtual void Update() {
        if(IsLocked && autoUnlock)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
                Unlock(Vector3.forward);
        } 
    }

    public void Open(Vector3 position)
    {
        if(!isOpen) ToggleDoor(position);
    }

    public void Close(Vector3 position)
    {
        if(isOpen) ToggleDoor(position);
    }

    public override void Lock(Vector3 position)
    {
        timer = autoUnlockTime;
        Close(position);
        if(!IsLocked) ToggleLock();
    }

    public override void Unlock(Vector3 position) 
    {
        if(IsLocked)
        {
            ToggleLock();
            Open(position);
        }
    } 

    public override bool GetState() => isOpen;

    public override string GetDescription() => "door"; //(IsLocked ? "locked" : (isOpen ? "open" : "close"));

    public override void Interact(Vector3 position) => ToggleDoor(position);
}
