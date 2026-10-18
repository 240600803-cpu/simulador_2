using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

// Simple serial reader for a controller that sends lines like: "LX:0.12,LY:-0.34,ROT:0.5,B1:1"
public class SerialController : MonoBehaviour
{
    public string portName = "COM3";
    public int baudRate = 115200;
    private SerialPort serial;
    private Thread thread;
    private bool running = false;
    private string latestLine = null;
    private object lockObj = new object();

    void Start()
    {
        try
        {
            serial = new SerialPort(portName, baudRate);
            serial.ReadTimeout = 200;
            serial.Open();
            running = true;
            thread = new Thread(ReadThread);
            thread.Start();
            Debug.Log("SerialController: Opened " + portName);
        }
        catch (Exception e)
        {
            Debug.LogWarning("SerialController failed to open: " + e.Message);
        }
    }

    void ReadThread()
    {
        while (running)
        {
            try
            {
                string line = serial.ReadLine();
                lock (lockObj) { latestLine = line; }
            }
            catch (TimeoutException) { }
            catch (Exception ex)
            {
                Debug.LogError("Serial read error: " + ex.Message);
                running = false;
            }
        }
    }

    void Update()
    {
        string line = null;
        lock (lockObj)
        {
            if (!string.IsNullOrEmpty(latestLine))
            {
                line = latestLine;
                latestLine = null;
            }
        }

        if (line != null)
        {
            // TODO: parse and expose values via events or public properties
            // Example parse into debug
            Debug.Log("Serial: " + line);
        }
    }

    void OnDestroy()
    {
        running = false;
        if (thread != null && thread.IsAlive) thread.Join(200);
        if (serial != null && serial.IsOpen) serial.Close();
    }
}
