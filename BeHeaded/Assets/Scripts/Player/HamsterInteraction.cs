using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterInteraction : PlayerInteraction
{
    public override void HandleInteraction(Interactable interactable, RaycastHit hit)
    {
        bool interactionDone = false;

        if(hit.collider.tag == "Switch")
        {
            if(Input.GetButtonDown("Interact"))
            {
                interactable.Interact(transform.position);
                interactionDone = true;
            }
                
            else if(Input.GetButtonDown("RepareSwitch"))
            {
                interactable.Repare();
                interactionDone = true;
            }
        }

        if(hit.collider.tag == "Door")
        {
            if(Input.GetButtonDown("Interact"))
            {
                interactable.Interact(transform.position);
                interactionDone = true;
            }
                
            else if(Input.GetButtonDown("LockDoor"))
            {
                interactable.Lock(transform.position); 
                interactionDone = true;
            }
        }

        if(interactionDone) enemyAI.NotifyEnemy(transform.position);
            
    }
}
