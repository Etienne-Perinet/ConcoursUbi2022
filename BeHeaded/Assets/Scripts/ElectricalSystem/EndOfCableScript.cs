using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfCableScript : Conductor
{
    private float waterTimer = 1;

    public float maximumWaterTime = 20;
    public float minimumWaterTime = 5;

    public float cableGettingWetSpeed = 1;
    public float cableGettingDrySpeed = 1;

    public List<Conductor> otherElementsConnected;
    public MiddleOfCableScript currentCable;

    private bool currentlyConnectedByWater = false;

    public bool alwaysConnected = true;
    private const string ElectricityRecepterTag = "ElectricalDoor";

    

    private void Start()
    {
        waterTimer = maximumWaterTime;
        cableGettingWetSpeed *= 10;
        tag = "CableConnector";
    }

    private void UseWater(Collider other)
    {

        if (other.gameObject.CompareTag("WaterBullet"))
        {
           
            Debug.Log("Water timer |endOfCable : " + waterTimer);
            if (waterTimer + cableGettingWetSpeed < maximumWaterTime)
            {
                waterTimer += cableGettingWetSpeed;
                Debug.Log("Adding Water");
            }
            else
            {
                waterTimer = maximumWaterTime;
                Debug.Log("Max Water");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AddCableIfPossible(other);
        UseWater(other);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (waterTimer > 0)
        {
            //Eleve de l'eau
            waterTimer -= cableGettingDrySpeed * Time.deltaTime;
            if (minimumWaterTime < waterTimer)
            {
                currentlyConnectedByWater = true;
            }
            else
            {
                if (isConducting)
                {
                    currentlyConnectedByWater = false;
                    //Pas assez d'eau
                    SendNoElectricity(this);
                }
            }
        }
        else
        {
            waterTimer = 0;
        }
    }

    //Look if they both have the same tag and if so add
    private void AddCableIfPossible(Collider collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            otherElementsConnected.Add(collision.gameObject.GetComponent<EndOfCableScript>().currentCable);
        }
        if (collision.gameObject.CompareTag(ElectricityRecepterTag))
        {
            otherElementsConnected.Add(collision.gameObject.GetComponent<Conductor>());
        }
    }

    public override void SendElectricity(Conductor sender)
    {
        if (!isConducting)
        {
            isConducting = true;            
            foreach (Conductor item in otherElementsConnected)
            {
                bool test1 = item.IsConnected(currentCable);
                bool test2 = sender != item;
                bool test3 = (alwaysConnected || currentlyConnectedByWater);
                if (item.IsConnected(currentCable) && sender != item && (alwaysConnected || currentlyConnectedByWater))
                {
                    item.SendElectricity(this);
                }
            }
        }
    }

    public override void SendNoElectricity(Conductor sender)
    {
        if (isConducting)
        {
            isConducting = false;
            foreach (Conductor item in otherElementsConnected)
            {
                if (item.IsConnected(currentCable) && sender.gameObject != item.gameObject)
                {
                    item.SendNoElectricity(currentCable);
                }
            }
        } 
    }

    public override bool IsConnected(Conductor otherEnd)
    {
        return true;// À revoir
    }
}
