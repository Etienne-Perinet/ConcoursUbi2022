using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElectricalDoor : Door
{
    private Vector3 defaultPosition;
    private Vector3 upPosition;
    private Vector3 targetPosition;
    [SerializeField] private float upTransition = 4f; 


    void Awake()
    {
        defaultPosition = transform.position;
        targetPosition = defaultPosition;

        upPosition = defaultPosition;
        upPosition.y += upTransition; 

        timer = 0;
    }

    void FixedUpdate() 
    {
        transform.position = targetPosition;
    }

    public override void ToggleDoor(Vector3 position)
    {
        Debug.Log("Toogle Electrical Door ");
        if(!IsLocked && !IsBroken) 
        {
            isOpen = !isOpen;
            targetPosition = (isOpen ? upPosition : defaultPosition);
        }
    }

    public override void Unlock(Vector3 position) 
    {
        if(IsLocked)
        {
            base.Unlock(position);
            gameObject.GetComponentInParent<NavMeshObstacle>().carving = false;
        }
    }

    
}
