using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVWriter
{
    public string FileName { get; private set; }
    public TextWriter writer { get; set; }
    public struct Data 
    {
        public Data(int miliseconds, float x, float y, float z)
        {
            Miliseconds = miliseconds;
            X = x;
            Y = y;
            Z = z;
        }

        public float Miliseconds { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    } 
    public CSVWriter(string filename)
    {
        FileName = Application.persistentDataPath + "/" + filename + ".csv";
        writer = new StreamWriter(FileName, true);
        writer.WriteLine("Time; X; Y; Z");
    }

    public void serilize(Data data)
    {
        writer.WriteLine($"{data.Miliseconds}; {data.X}; {data.Y}; {data.Z}");
    }
}
