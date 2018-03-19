using ArduinoUploader.Hardware;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID_Client_Arduino_Console
{
    class Program
    {
        private const ArduinoModel AttachedArduino = ArduinoModel.Mega2560;
        private static SerialPort serialPort;
        private delegate void LineReceivedEvent(string line);
        static void Main(string[] args)
        {
            serialPort = new SerialPort();
            serialPort.PortName = "COM7";
            serialPort.BaudRate = 9600;
            serialPort.DtrEnable = true;
            serialPort.Open();
            serialPort.DataReceived += SerialPort7_DataReceived;
        }

        private static void SerialPort7_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("Begin receiving data...");
            Console.WriteLine(serialPort.ReadExisting());
            Console.WriteLine("End receiving!");

            serialPort.Close();
        }
    }
}

//namespace Read_serial
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();

//            serialPort1.PortName = "COM4";
//            serialPort1.BaudRate = 9600;
//            serialPort1.DtrEnable = true;
//            serialPort1.Open();

//            serialPort1.DataReceived += serialPort1_DataReceived;
//        }

//        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
//        {
//            string line = serialPort1.ReadLine();
//            this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
//        }

//        private delegate void LineReceivedEvent(string line);
//        private void LineReceived(string line)
//        {
//            //What to do with the received line here
//            label1.Text = line;

//            progressBar1.Value = int.Parse(line);
//        }
//    }
//}
