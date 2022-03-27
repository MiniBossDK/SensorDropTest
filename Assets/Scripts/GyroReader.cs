using UnityEngine;
using System;


public class GyroReader
{
    public Gyroscope gyro { get; private set; }
    public bool isEnabled = false;

    public GyroReader()
    {
        if (!SystemInfo.supportsGyroscope)
        {
            isEnabled = false;
            return;
            //throw new Exception("This device does not have a gyro sensor.");
        }
        isEnabled = true;

        gyro = Input.gyro;
        gyro.enabled = true;
    }

    public bool IsLayingFlat(float threashold)
    {
        Quaternion q = gyro.attitude;
        return (q.x <= threashold && q.x >= -threashold) &&
            (q.y <= threashold && q.y >= -threashold);
    }

    
}
