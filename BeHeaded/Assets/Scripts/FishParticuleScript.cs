using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishParticuleScript : MonoBehaviour
{
    public GameObject particuleSystem;
    public Transform puddleTransform;

    float destroyTime = 5;
    float HeightVisualWaterSpawn = 0.102f;

    private float waterTimer = 2;

    public float maximumWaterTime = 20;

    private float cableGettingWetSpeed = EndOfCableScript.cableGettingWetSpeed;
    private float cableGettingDrySpeed = EndOfCableScript.cableGettingDrySpeed;

    Vector3 MaxScaleOfWaterOnTheGround = new Vector3(1, 0.2383617f, 1);


    private void Start()
    {
        Destroy(particuleSystem, destroyTime);
       // MaxScaleOfWaterOnTheGround *= 2;
        transform.position = new Vector3(transform.position.x, HeightVisualWaterSpawn, transform.position.z);
        puddleTransform.localScale = MaxScaleOfWaterOnTheGround / 10;
    }

    private void Update()
    {
        puddleTransform.localScale = MaxScaleOfWaterOnTheGround * (waterTimer / maximumWaterTime);
        if (waterTimer > 0)
        {
            waterTimer -= cableGettingDrySpeed * Time.deltaTime;
        }
        else
        {
            waterTimer = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        UseWater(other);
    }

    private void UseWater(Collider other)
    {
        Debug.Log("Entered the water on the ground : " + other.name);
        if (other.gameObject.CompareTag("WaterBullet"))
        {
            Debug.Log("WaterTimer = " + waterTimer);
            Destroy(other);
            if (waterTimer + cableGettingWetSpeed < maximumWaterTime)
            {
                waterTimer += cableGettingWetSpeed;
            }
            else
            {
                waterTimer = maximumWaterTime;
            }
        }

    }
}
