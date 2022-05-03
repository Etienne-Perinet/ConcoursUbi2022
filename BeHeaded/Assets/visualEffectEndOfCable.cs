using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualEffectEndOfCable : VisualEffectCablePiece
{
    public new void ChangeColor()
    {

        renderer.materials[0] = electricMaterial;
        renderer.materials[1] = electricMaterial;
        StartCoroutine(CoroutineToChangeBackMaterial());
    }

    protected new IEnumerator CoroutineToChangeBackMaterial()
    {
        yield return new WaitForSeconds(timeChangingColors);

        renderer.material = normalMaterial;
    }

}
