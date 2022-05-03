using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateXRay : MonoBehaviour
{
    Camera XRayCamera;
    private void Start()
    {
        XRayCamera = GameObject.Find("XRayCamera").GetComponent<Camera>();
    }


    void Update()
    {
        //XRayCamera.enabled = false;
        
        //XRayCamera.enabled = false;
    }

     
}
