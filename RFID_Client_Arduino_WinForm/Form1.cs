using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID_Client_Arduino_WinForm
{
    public partial class Form1 : Form
    {
        private static SerialPort serialPort;
        private delegate void LineReceivedEvent(string line);
        private delegate void ErrorReceivedEvent(SerialErrorReceivedEventArgs e);

        public Form1()
        {
            InitializeComponent();

            serialPort = new SerialPort();
            serialPort.PortName = "COM7";
            //serialPort.BaudRate = 9600;
            serialPort.DtrEnable = true;

        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.BeginInvoke(new ErrorReceivedEvent(GetSerialError), e);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string line = serialPort.ReadExisting();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            PortOpen();
        }

        private void PortOpen()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.ErrorReceived += SerialPort_ErrorReceived;

                    serialPort.Open();
                    lblReceiveBuffer.Text = GetPortConfig() + Environment.NewLine + "Port opened successfully!";
                }
                else
                {
                    lblReceiveBuffer.Text = GetPortConfig() + Environment.NewLine + "Port already opened!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            PortClose();
        }

        private void PortClose()
        {
            if (serialPort.IsOpen)
            {
                try
                {

                    serialPort.Close();
                    serialPort.DataReceived -= SerialPort_DataReceived;
                    serialPort.ErrorReceived -= SerialPort_ErrorReceived;

                    lblReceiveBuffer.Text = "Port closed successfully!";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error closing port");
                }
            }
            else
            {
                lblReceiveBuffer.Text = "Port already closed!";
            }
        }

        private void LineReceived(string line)
        {
            //What to do with the received line here
            lblReceiveBuffer.Text += line;
        }

        private string GetPortConfig()
        {
            StringBuilder str = new StringBuilder();

            str.Append("Port number: ");
            str.Append(serialPort.PortName.ToString());
            str.AppendLine();

            str.Append("Baud rate: ");
            str.Append(serialPort.BaudRate.ToString());
            str.AppendLine();

            str.Append("Stop bits: ");
            str.Append(serialPort.StopBits.ToString());
            str.AppendLine();

            str.Append("DTR enable: ");
            str.Append(serialPort.DtrEnable.ToString());
            str.AppendLine();

            return str.ToString();
        }

        private void GetSerialError(SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblReceiveBuffer.Text = "";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            PortClose();
        }
    }
}
