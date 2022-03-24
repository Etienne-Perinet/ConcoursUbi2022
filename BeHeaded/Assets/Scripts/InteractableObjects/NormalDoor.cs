using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : Door
{
    [SerializeField] protected float rotationDirection = -1f; // direction de l'ouverture de la porte (1, -1)
    [SerializeField] protected float smooth = 10f;
    [SerializeField] protected float autoCloseTime = 5f;
    [SerializeField] protected Transform pivot;
    
    protected float targetYRotation;
    protected float defaultYRotation;
    
    void Start()
    {
        defaultYRotation = transform.eulerAngles.y;
        timer = 0;
    }

    public override void Update()
    {
        var newRotation = Quaternion.Euler(0f, defaultYRotation + targetYRotation, 0f);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, newRotation,  smooth * Time.deltaTime);

        base.Update();
    }

    public override void ToggleDoor(Vector3 position)
    {
        if(!IsLocked && !IsBroken) 
        {
            isOpen = !isOpen;

            if(isOpen)
            {
                Vector3 direction = position - transform.position;
                targetYRotation = rotationDirection * Mathf.Sign(Vector3.Dot(transform.right, direction)) * 90f;
                StartCoroutine(AutoClose(position, autoCloseTime));
            } 
            else targetYRotation = 0f;
        }
    }

    IEnumerator AutoClose(Vector3 position, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Close(position);
    }
}
