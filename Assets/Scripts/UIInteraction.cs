using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI buttonText;
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
    private AccelerometerReader accelerometerReader;
    private bool isMeasuring = false;

    private void Start()
    {
        accelerometerReader = new AccelerometerReader();
        gyroReader = new GyroReader();
        successImage.SetActive(false);
        
        button.onClick.AddListener(StartMeasuringData);
        sensorStates.text = $"Gyroscope: {gyroReader.isEnabled}" + Environment.NewLine +
                            $"Accelerometer: {accelerometerReader.isEnabled}";

    }

    void OutputSensorData()
    {

        if(accelerometerReader.isEnabled)
        {
            sensorDataAcceleration.text = $"Accelerometer:" + Environment.NewLine +
                                      $"X: {accelerometerReader.accelaration.x}" + Environment.NewLine +
                                      $"Y: {accelerometerReader.accelaration.y}" + Environment.NewLine +
                                      $"Z: {accelerometerReader.accelaration.z}";
        }
        
        if(gyroReader.isEnabled)
        {
            sensorDataRotation.text = $"Gyroscope:" + Environment.NewLine +
                                  $"X: {gyroReader.gyro.x}" + Environment.NewLine +
                                  $"Y: {gyroReader.gyro.y}" + Environment.NewLine +
                                  $"Z: {gyroReader.gyro.z}";
        }
        
    }

    void StartMeasuringData()
    {
        csvWriter = new CSVWriter(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));
        isMeasuring = true;
        buttonText.text = "Reading...";
        button.image.color = Color.yellow;
    }

    private void Update()
    {
        OutputSensorData();
        if(isMeasuring)
        {
            if (gyroReader.IsLayingFlatUp() || data.Count >= dataLimit)
            {
                isMeasuring = false;
                return;
            }
            data.Add(accelerometerReader.accelaration);
        }
        else
        {
            if(data.Count > 0)
            {
                csvWriter.serilize(data); // Serilize all the data to csv format
                csvWriter.writer.Close(); // Close the writer
                data.Clear(); // Dumps the already written data
                buttonText.text = "Start";
                button.image.color = Color.green;
                StartCoroutine(ShowSuccess()); // Show that the data has been written
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
