using System;
using System.IO.Ports;

public class SensorService
{
    private SerialPort serialPort;

    public SensorService(string portName)
    {
        serialPort = new SerialPort(portName, 9600);
       // serialPort.Open();
    }

    public string ReadSensorData()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                return serialPort.ReadLine(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading serial data: " + ex.Message);
            }
        }
        return null;
    }
}
