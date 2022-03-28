using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTest : MonoBehaviour
{
    private SensorManager sensorManager;

    private void Awake()
    {
        sensorManager = SensorManager.Instance;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
