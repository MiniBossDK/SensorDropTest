using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class AccelerometerReader
{
    private Vector3 Accelaration;
    public Vector3 accelaration 
    {
        get
        {
            return Accelerometer.current.acceleration.ReadValue();
        }
    }

    public Accelerometer accelarationSensor { get; private set; }

    public bool isEnabled = false;

    public AccelerometerReader()
    {
        if(Accelerometer.current == null)
        {
            isEnabled = false;
            return;
            //throw new Exception("This device does not have a accelrometer sensor.");
        }

        InputSystem.EnableDevice(Accelerometer.current);

        accelarationSensor = Accelerometer.current;

        isEnabled = true;
    }
}
