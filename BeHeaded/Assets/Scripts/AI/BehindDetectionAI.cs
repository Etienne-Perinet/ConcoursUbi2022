using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindDetectionAI : MonoBehaviour
{
    public bool PlayerDetected { get; protected set; }

    void Start() => PlayerDetected = false;

    void OnTriggerEnter(Collider other) => PlayerDetected = (other.tag == "Player");
}
