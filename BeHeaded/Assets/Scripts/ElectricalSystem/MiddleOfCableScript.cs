using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleOfCableScript : Conductor
{
    //Size 2
    public GameObject[] EndOfCableObject;
    private EndOfCableScript[] EndOfCable = new EndOfCableScript[2];

    private bool isElectricityPassingThrough = false;
    private Conductor lastObjectComunicatedWith;

    private void Start()
    {
        EndOfCable[0] = EndOfCableObject[0].GetComponent<EndOfCableScript>();
        EndOfCable[1] = EndOfCableObject[1].GetComponent<EndOfCableScript>();


        EndOfCable[0].currentCable = this;
        EndOfCable[1].currentCable = this;

        EndOfCable[0].indexEndOfCable = 0;
        EndOfCable[1].indexEndOfCable = 1;
    }

   

    public override void SendElectricity(Conductor sender) { //Sender = endOfCable

        if (!(sender is null))
        {
            Debug.Log("Electricity received on " + name + " from " + sender.name);
        }
        else
        {
            Debug.Log("FirstTimeReceiving electrycity, sending electricity");
        }
        if (!(sender is null) && sender.indexEndOfCable != -1)
        {
            EndOfCable[sender.indexEndOfCable].SendElectricity(this);
        }
        else
        {
            foreach (EndOfCableScript item in EndOfCable)
            {
                item.SendElectricity(this);
            }
        }
        isElectricityPassingThrough = true;
    }

    public override bool IsConnected(Conductor otherEnd)
    {
        return EndOfCable[0].otherElementsConnected.Contains(otherEnd) || EndOfCable[1].otherElementsConnected.Contains(otherEnd);
    }

    public override void SendNoElectricity(Conductor sender)
    {
        isElectricityPassingThrough = false;
        
        if (!(sender is null))
        {
            // Debug.Log("Electricity received on " + name + " from " + sender.name);
        }
        if (!(sender is null) && sender.indexEndOfCable != -1)
        {
            EndOfCable[sender.indexEndOfCable].SendNoElectricity(this);
        }
        else
        {
            foreach (EndOfCableScript item in EndOfCable)
            {
                item.SendNoElectricity(this);
            }
        }
    }
}
