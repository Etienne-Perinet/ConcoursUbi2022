using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateControllerComponent : MonoBehaviour
{
    public int targetFrameRate = 60;
    public int isVsyncEnabled = 0;
    
    void Awake()
    {
        QualitySettings.vSyncCount = isVsyncEnabled;
        Application.targetFrameRate = targetFrameRate;
    }
}
