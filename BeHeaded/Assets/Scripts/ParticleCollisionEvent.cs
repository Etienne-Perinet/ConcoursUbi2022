using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionEvent : MonoBehaviour
{

    //public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        //collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("I'm HIT!!");
    }
}
