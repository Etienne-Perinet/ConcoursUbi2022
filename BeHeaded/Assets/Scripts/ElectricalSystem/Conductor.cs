using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conductor : MonoBehaviour
{
    public int indexEndOfCable = -1;
    protected bool isConducting = false;

    public abstract void SendElectricity(Conductor sender);
    public abstract void SendNoElectricity(Conductor sender);
    public abstract bool IsConnected(Conductor otherEnd);

    public static bool operator ==(Conductor conductor1, Conductor conductor2)
    {
        if (!(conductor1 is null|| conductor2 is null))
        {
            return (conductor1.gameObject == conductor2.gameObject);
        }
        return false;   
        //return !(conductor1 is null || conductor2 is null) && (conductor1.gameObject == conductor2.gameObject);
    }

    public static bool operator !=(Conductor conductor1, Conductor conductor2)
    {
        return !(conductor1 == conductor2);
    }
}
