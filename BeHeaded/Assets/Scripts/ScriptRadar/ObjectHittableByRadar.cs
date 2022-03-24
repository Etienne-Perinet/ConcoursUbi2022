using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHittableByRadar : RadarSweepTriggerable
{
    public GameObject point;

    public override void OnSwipe()
    {        
        GameObject realPoint = Instantiate(point,transform.position, transform.rotation * Quaternion.Euler(90, 0, 0)) as GameObject;
        // Debug.Log("I got hit!");
    }
}
