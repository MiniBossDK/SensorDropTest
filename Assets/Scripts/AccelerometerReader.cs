using UnityEngine;
using System;

public class AccelerometerReader
{
    public Vector3 accelaration 
    {
        get
        {
            return Input.acceleration;
        }
    }

    public bool isEnabled = false;

    public AccelerometerReader()
    {
        if(!SystemInfo.supportsAccelerometer)
        {
            isEnabled = false;
            return;
            //throw new Exception("This device does not have a accelrometer sensor.");
        }
        

        isEnabled = true;
    }
}
