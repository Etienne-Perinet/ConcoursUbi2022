using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    private float timer = 0;
    private float maxTimer = 9;
    
    public Color color;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < maxTimer)
        {
            timer += Time.deltaTime;
            color.a = CalculateAlpha();
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private float CalculateAlpha()
    {
        return (-0.3f)*((timer - 1f) * (timer - 1f)) + 1;
    }

}
