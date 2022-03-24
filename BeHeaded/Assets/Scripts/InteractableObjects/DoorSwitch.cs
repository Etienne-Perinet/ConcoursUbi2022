using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : Switch
{
    [SerializeField] protected Interactable interactable; // Object que la switch affecte avec son état

    public override void UpdateObject() => interactable.Interact(Vector3.forward);

    public override string GetDescription() 
    {
        if(!IsBroken)
            return "Press [Q] to " + (interactable.GetState() ? "close" : "open") + " the door";
        return "Press [R] to repare the switch";
    } 

}
