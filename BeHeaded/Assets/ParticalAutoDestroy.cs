using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalAutoDestroy : MonoBehaviour
{
    float destroyTime = 5;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    
}
