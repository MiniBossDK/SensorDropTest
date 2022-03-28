using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class GyroReader
{
    public Quaternion gyro 
    { 
        get
        {
            return AttitudeSensor.current.attitude.ReadValue();
        }
            
    }
    public Gyroscope gyroscopeSensor { get; private set; }
    public bool isEnabled = false;

    public GyroReader()
    {
        if (Gyroscope.current == null)
        {
            isEnabled = false;
            return;
            //throw new Exception("This device does not have a gyro sensor.");
        }
        isEnabled = true;

        InputSystem.EnableDevice(AttitudeSensor.current);

        gyroscopeSensor = Gyroscope.current;
    }

    /**
     * <summary>
     * Checks weather the the phone is laying flat down with the screen up and with a given threshold.<br/>
     * The threadshold defaults to 0.05;
     * </summary>
     */
    public bool IsLayingFlatUp(float threashold = 0.05f)
    {
        Quaternion q = gyro;
        return (q.x <= threashold && q.x >= -threashold) &&
            (q.y <= threashold && q.y >= -threashold);
    }
}
