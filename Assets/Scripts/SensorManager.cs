using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class SensorManager : Singleton<SensorManager>
{
    public delegate void AttitudeChangedEvent(Quaternion quaternion);
    public event AttitudeChangedEvent OnAttitudeChanged;

    public delegate void AccelerationChangedEvent(Vector3 quaternion);
    public event AccelerationChangedEvent OnAccelerationChanged;

    Input inputSensors;

    private void Awake()
    {
        inputSensors = new Input();
    }

    private void OnEnable()
    {
        inputSensors.Enable();
    }

    private void OnDisable()
    {
        inputSensors.Disable();
    }

    void Start()
    {
        inputSensors.Sensors.Acceleration.performed += AccelerationChanged;
        inputSensors.Sensors.Attitude.performed += AttitudeChanged;
    }

    private void AccelerationChanged(InputAction.CallbackContext context)
    {
        if (OnAccelerationChanged != null) OnAccelerationChanged(context.ReadValue<Vector3>());
    }
    private void AttitudeChanged(InputAction.CallbackContext context)
    {
        if (OnAttitudeChanged != null) OnAttitudeChanged(context.ReadValue<Quaternion>());
    }

}
