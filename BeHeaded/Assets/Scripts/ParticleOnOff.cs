using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnOff : MonoBehaviour
{
    private ParticleSystem particle;
    //private bool shootParticle;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        //shootParticle = true;
        Debug.Log("SOY LIVE");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("YES");
            particle.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("NO");
            particle.Stop();
        }
    }

    /* private void FixedUpdate()
     {
         if (shootParticle)
         {
            particle.Play();
         }
         else
         {
             particle.Stop();
         }

     }*/

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Gun hit!");
    }
}
