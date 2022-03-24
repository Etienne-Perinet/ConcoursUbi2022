using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionBarAI : MonoBehaviour
{
    [SerializeField] private Image bar;
    
    [SerializeField] private RectTransform button;

    void Start() => ReinitializeBar();

    public void ReinitializeBar() 
    {
        bar.fillAmount = 0;
        button.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void DetectionChange(float detectionValue)
    {
        float amount = (detectionValue / 100f) * 180f/360;

        bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }
}
