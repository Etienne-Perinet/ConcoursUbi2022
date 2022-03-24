using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalSwitch : Switch
{
    [SerializeField] protected Conductor cableConnected; // Object que la switch affecte avec son état

    public override void UpdateObject()
    {
        cableConnected.SendElectricity(null);
    }
        

    public override string GetDescription() => GetState().ToString();

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "WaterBubble" || collision.gameObject.name == "WaterBubble(Clone)")
        {
            Debug.Log("Interact from ElectricalSwitch with " + collision.gameObject.name);
            Interact(Vector3.zero);
        }
    }
}
