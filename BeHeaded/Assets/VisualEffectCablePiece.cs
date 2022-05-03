using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectCablePiece : MonoBehaviour
{
    public Renderer renderer;
    public Material normalMaterial;
    public Material electricMaterial;

    public float timeChangingColors = 4;

    private void Start()
    {
        renderer.material = normalMaterial;
    }

    public void ChangeColor()
    {
        renderer.material = electricMaterial;
        Debug.Log("Changing Color");
        StartCoroutine(CoroutineToChangeBackMaterial());
    }
    protected IEnumerator CoroutineToChangeBackMaterial()
    {
        yield return new WaitForSeconds(timeChangingColors);
        Debug.Log("Get old Color");
        renderer.material = normalMaterial;
    }
 
}
