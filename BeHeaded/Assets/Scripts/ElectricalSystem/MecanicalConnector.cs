using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicalConnector : Conductor
{
    public ElectricalDoor objectToInteractWith;
    public override bool IsConnected(Conductor otherEnd)
    {
        return true;
    }

    public override void SendElectricity(Conductor sender)
    {
        if(!isConducting)
        {
            objectToInteractWith.Unlock(Vector3.forward);
            Debug.Log("I am open");
            isConducting = !isConducting;
        }
            
    }

    public override void SendNoElectricity(Conductor sender)
    {
        if(isConducting)
        {
            isConducting = !isConducting;
        }
    }
}
