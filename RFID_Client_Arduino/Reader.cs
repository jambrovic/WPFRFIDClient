using System.IO.Ports;

namespace RFIDClient.Arduino
{
    class Reader : IReader
    {
        private SerialPort m_SerialPort;
        private string m_PortName;
        private int m_PortBaudRate;
        private bool m_UseDTR;

        public event ReaderEventHandler onReaderDataReceived;

        public Reader(string portName, int baudRate, bool useDTR)
        {
            m_SerialPort = new SerialPort();
            m_SerialPort.PortName = portName;
            m_SerialPort.BaudRate = baudRate;
            m_SerialPort.DtrEnable = useDTR;

            m_PortName = portName;
            m_PortBaudRate = baudRate;
            m_UseDTR = useDTR;

            m_SerialPort.DataReceived += _serialPort_DataReceived;
        }

        public void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.onReaderDataReceived.Invoke(m_SerialPort.ReadExisting());
        }

        public void OpenCommunication()
        {
            OpenPort();
        }

        public void CloseCommunication()
        {
            ClosePort();
        }

        public IReader GetReader(string portName, int baudRate, bool dtrEnable)
        {
            return new Reader(portName, baudRate, dtrEnable);
        }

        /// <summary>
        /// Open serial port
        /// </summary>
        /// <returns></returns>
        private bool OpenPort()
        {
            //TODO: Check first if port exists
            if (!m_SerialPort.IsOpen)
            {
                m_SerialPort.Open();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Close serial port
        /// </summary>
        /// <returns></returns>
        private bool ClosePort()
        {
            //TODO: Check first if port exists
            if (m_SerialPort.IsOpen)
            {
                m_SerialPort.Close();
                m_SerialPort.DataReceived -= _serialPort_DataReceived;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if serial port is opened
        /// </summary>
        /// <returns></returns>
        public bool IsPortOpen()
        {
            //TODO: Check first if port exists
            return m_SerialPort.IsOpen;
        }

    }
}
