using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;
using System;

public class RadarScript : MonoBehaviour
{
    public float rotationSpeed = 5;
    public float minimumTimeBetweenDetection = 5;

    private GameObject sweepObject;
    private GameObject endOfSweepObject;
    private List<ObjectTouchedByRadarSweep> colliderListThatWereHit = new List<ObjectTouchedByRadarSweep>();

    void Awake()
    {
        sweepObject = GameObject.Find("SweepLine");
        endOfSweepObject = GameObject.Find("EndOfSweep");
    }

    void Update()
    {
        sweepObject.transform.eulerAngles += new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        Ray ray = new Ray(sweepObject.transform.position, GetDirection());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("RadarHit")) && CanTrigger(hit.collider))
        {
           hit.collider.gameObject.GetComponent<RadarSweepTriggerable>().OnSwipe();
        }
    }

    private bool CanTrigger(Collider collider)
    {
        if (collider.gameObject.GetComponent<RadarSweepTriggerable>() != null)
        {
            ObjectTouchedByRadarSweep newCollider = new ObjectTouchedByRadarSweep(collider);
            if (colliderListThatWereHit.Contains(newCollider))
            {
                foreach (ObjectTouchedByRadarSweep item in colliderListThatWereHit)
                {
                    if ((item.Equals(collider)) && (minimumTimeBetweenDetection < (Time.realtimeSinceStartup - item.startingCountdownSinceStartOfLauch)))
                    {
                        Debug.Log("Time since last hit : " + (Time.realtimeSinceStartup - item.startingCountdownSinceStartOfLauch));
                        item.startingCountdownSinceStartOfLauch = Time.realtimeSinceStartup;
                        return true;
                    }
                }
            }
            else
            {
                colliderListThatWereHit.Add(new ObjectTouchedByRadarSweep(collider));
                return true;
            }
        }
        return false;
    }

    private Vector3 GetDirection()
    {
        return endOfSweepObject.transform.position - sweepObject.transform.position;
    }

    private class ObjectTouchedByRadarSweep : IEquatable<ObjectTouchedByRadarSweep>
    {
        public float startingCountdownSinceStartOfLauch { get; set; }

        public Collider colliderFromObjectTouched { get; set; }

        public ObjectTouchedByRadarSweep(Collider colliderFromObjectTouched)
        {
            this.colliderFromObjectTouched = colliderFromObjectTouched;
            startingCountdownSinceStartOfLauch = Time.realtimeSinceStartup;
        }

        public override bool Equals(object obj)
        {
            try
            {
                int test1 = obj.GetHashCode();
                int test2 = colliderFromObjectTouched.GetHashCode();
                if (obj.GetHashCode() == colliderFromObjectTouched.GetHashCode())
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public override int GetHashCode()
        {
            return colliderFromObjectTouched.GetHashCode();
        }

        public bool Equals(ObjectTouchedByRadarSweep other)
        {
            return this.Equals((object)other);
        }
    }
}
