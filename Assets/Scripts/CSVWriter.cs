using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVWriter
{
    public string FileName { get; private set; }
    private const string FileFolder = "/Measurements/";
    public TextWriter writer { get; private set; }

    public CSVWriter(string filename)
    {
        FileName = Application.persistentDataPath + "/" + filename + ".csv";
        writer = new StreamWriter(FileName, true);
        //Directory.CreateDirectory(Application.persistentDataPath + FileFolder);
    }

    public void serilize(List<Vector3> vectors)
    {

        writer.WriteLine("X; Y; Z");

        foreach (Vector3 v in vectors)
        {
            writer.WriteLine($"{v.x}; {v.y}; {v.z}");
        }
    }

    public void serilize(List<Quaternion> quaternions)
    {
        writer.WriteLine("X; Y; Z; W");

        foreach (Quaternion q in quaternions)
        {
            writer.WriteLine($"{q.x}; {q.y}; {q.z}; {q.w}");
        }
    }
}
