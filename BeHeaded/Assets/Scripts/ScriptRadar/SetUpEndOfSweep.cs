using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpEndOfSweep : MonoBehaviour
{
    private GameObject sweepObject;
    // Start is called before the first frame update
    void Start()
    {
        sweepObject = GameObject.Find("SweepLine");
        gameObject.transform.position =
            -1*(sweepObject.transform.position + sweepObject.GetComponent<Collider>().bounds.size) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
