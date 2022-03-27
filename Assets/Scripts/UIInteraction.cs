using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI sensorDataRotation;
    [SerializeField]
    private TextMeshProUGUI sensorDataAcceleration;
    [SerializeField]
    private TextMeshProUGUI sensorStates;
    [SerializeField]
    private GameObject successImage;



    private const int dataLimit = 500;

    private List<Vector3> data = new List<Vector3>();

    private CSVWriter csvWriter;
    private GyroReader gyroReader;
    private AccelerometerReader accelerometer;
    private bool isMeasuring = false;

    private void Start()
    {
        accelerometer = new AccelerometerReader();
        gyroReader = new GyroReader();
        successImage.SetActive(false);
        
        button.onClick.AddListener(StartMeasuringData);
        sensorStates.text = $"Gyroscope: {gyroReader.isEnabled}" + Environment.NewLine +
                            $"Accelerometer: {accelerometer.isEnabled}";

    }
    void OutputSensorData()
    {
        sensorDataAcceleration.text = $"Accelerometer:" + Environment.NewLine +
                                      $"X: {accelerometer.accelaration.x}" + Environment.NewLine +
                                      $"Y: {accelerometer.accelaration.y}" + Environment.NewLine +
                                      $"Z: {accelerometer.accelaration.z}";

        sensorDataRotation.text = $"Gyroscope:" + Environment.NewLine +
                                  $"X: {gyroReader.gyro.attitude.x}" + Environment.NewLine +
                                  $"Y: {gyroReader.gyro.attitude.y}" + Environment.NewLine +
                                  $"Z: {gyroReader.gyro.attitude.z}";
    }

    void StartMeasuringData()
    {
        csvWriter = new CSVWriter(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));
        isMeasuring = true;
    }

    private void Update()
    {
        OutputSensorData();
        if(isMeasuring)
        {
            if (gyroReader.IsLayingFlat(0.05f) || data.Count >= dataLimit)
            {
                isMeasuring = false;
                return;
            }
            data.Add(accelerometer.accelaration);
        }
        else
        {
            if(data.Count > 0)
            {
                csvWriter.serilize(data); // Serilize all the data to csv format
                csvWriter.writer.Close(); // Close the writer
                data.Clear(); // Dump the already written data
                StartCoroutine(ShowSuccess());
            }
        }
    }

    IEnumerator ShowSuccess()
    {
        successImage.SetActive(true);
        yield return new WaitForSeconds(5);
        successImage.SetActive(false);
    }
}
