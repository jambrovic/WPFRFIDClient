using System.IO.Ports;

namespace RFIDClient.Arduino
{
    /// <summary>
    /// RFID reader base class
    /// </summary>
    class Reader : IReader
    {
        #region Private Members
        
        /// <summary>
        /// Serial port instance
        /// </summary>
        private SerialPort m_SerialPort;

        /// <summary>
        /// Port name
        /// </summary>
        private string m_PortName;

        /// <summary>
        /// Baud rate of the serial port
        /// </summary>
        private int m_PortBaudRate;

        /// <summary>
        /// Data terminal ready flag
        /// </summary>
        private bool m_UseDTR;

        #endregion

        #region Public Events

        /// <summary>
        /// Event that triggers on data receive from reader
        /// </summary>
        public event ReaderEventHandler onReaderDataReceived;

        /// <summary>
        /// Event that is triggered when data is received
        /// </summary>
        /// <param name="sender">Sender of the data</param>
        /// <param name="e">Parameters</param>
        public void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.onReaderDataReceived.Invoke(m_SerialPort.ReadExisting());
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="portName">Name of the port</param>
        /// <param name="baudRate">Speed of the port</param>
        /// <param name="useDTR">Data terminal ready flag</param>
        public Reader(string portName, int baudRate, bool useDTR)
        {
            m_SerialPort = new SerialPort();
            m_SerialPort.PortName = portName;
            m_SerialPort.BaudRate = baudRate;
            m_SerialPort.DtrEnable = useDTR;

            m_PortName = portName;
            m_PortBaudRate = baudRate;
            m_UseDTR = useDTR;

            //Subscription to event
            m_SerialPort.DataReceived += _serialPort_DataReceived;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Open communication channel
        /// </summary>
        public void OpenCommunication()
        {
            OpenPort();
        }

        /// <summary>
        /// Close communication channel
        /// </summary>
        public void CloseCommunication()
        {
            ClosePort();
        }

        /// <summary>
        /// Gets the instance if IReader interface
        /// </summary>
        /// <param name="portName">Port name</param>
        /// <param name="baudRate">Port speed</param>
        /// <param name="dtrEnable">Data terminal ready flag</param>
        /// <returns></returns>
        public IReader GetReader(string portName, int baudRate, bool dtrEnable)
        {
            return new Reader(portName, baudRate, dtrEnable);
        }

        /// <summary>
        /// Check if serial port is opened
        /// </summary>
        /// <returns>Returns true if port is opened</returns>
        public bool IsCommunicationOpen()
        {
            //Check if port exists
            if (m_SerialPort != null)
            {
                //Return open state
                return m_SerialPort.IsOpen;
            }
            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Open serial port
        /// </summary>
        /// <returns>Returns true if success</returns>
        private bool OpenPort()
        {
            //Check if port exists and is closed
            if (m_SerialPort != null && !m_SerialPort.IsOpen)
            {
                //open port
                m_SerialPort.Open();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Close serial port
        /// </summary>
        /// <returns>Returns true if success</returns>
        private bool ClosePort()
        {
            //Check if port exists and is opened
            if (m_SerialPort != null && m_SerialPort.IsOpen)
            {
                m_SerialPort.Close();
                m_SerialPort.DataReceived -= _serialPort_DataReceived;
                return true;
            }

            return false;
        }

        #endregion

    }
}
