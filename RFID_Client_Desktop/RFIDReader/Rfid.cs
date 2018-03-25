using RFIDClient.Arduino;
using System;
using System.Threading;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// RFID reader class for handling connection to RFID Arduino module
    /// </summary>
    public class Rfid
    {
        #region Private Members

        /// <summary>
        /// Reader connection flag
        /// </summary>
        private bool m_IsRFIDReaderConnected;

        /// <summary>
        /// Instance of the IReader
        /// </summary>
        private IReader m_ReaderInstance;

        /// <summary>
        /// Buffer for storing scanned RFID codes
        /// </summary>
        private string m_ReceiveBuffer;

        /// <summary>
        /// Flag for signaling when receive of a single RFID code is complete
        /// </summary>
        private bool m_IsReceiveComplete = false;

        #endregion

        #region Public Events

        public event EventHandler<RfidEventArgs> OnDataReceived;
        public event EventHandler<ReaderEventArgs> OnConnectionChanged;


        #endregion        /// <summary>

        #region Constructor

        /// Default constructor
        /// </summary>
        public Rfid()
        {
            InitReader();
            StartConnectionMonitor();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// RFID reader connection monitor for signaling when connection breaks
        /// </summary>
        private void StartConnectionMonitor()
        {
            //Start in separate thread
            Thread readerThread = new Thread(() =>
            {
                //Check continuously
                while (true)
                {
                    //Reader instance exists and communication is closed
                    if (m_ReaderInstance != null && !m_ReaderInstance.IsCommunicationOpen())
                    {
                        //Set flag to false
                        m_IsRFIDReaderConnected = false;

                        //Unsubscribe from reader data received event
                        m_ReaderInstance.onReaderDataReceived -= m_ReaderInstance_onReaderDataReceived;

                        //Signal to subscribers that connection has changed
                        this.OnConnectionChanged.Invoke(this, new ReaderEventArgs(false));
                    }
                    else if (m_ReaderInstance != null)
                    {
                        //Signal to subscribers that connection is still opened
                        this.OnConnectionChanged.Invoke(this, new ReaderEventArgs(true));
                    }

                    //Sleep for 1 second
                    Thread.Sleep(1000);
                }
            })
            {
                IsBackground = true,
                Name = "ConnectionMonitorThread"
            };

            readerThread.Start();
        }


        /// <summary>
        /// Initializes RFID reader
        /// </summary>
        private void InitReader()
        {
            //Start in separate thread
            Thread readerThread = new Thread(() =>
            {
                //Check continuously
                while (true)
                {
                    
                    //If reader is not connected
                    if (!m_IsRFIDReaderConnected)
                    {
                        try
                        {
                            //Set port configuration and get instance of the RFID reader
                            m_ReaderInstance = ReaderFactory.GetInstance("COM7", 9600, true);

                            //Open port
                            if (m_ReaderInstance.OpenCommunication())
                            {
                                //Signal to subscribers that connection is opened
                                this.OnConnectionChanged.Invoke(this, new ReaderEventArgs(true));

                                //Init the receive buffer
                                m_ReceiveBuffer = String.Empty;

                                //Check if reader instance exists
                                if (m_ReaderInstance != null)
                                {
                                    //Subscribe to data received event
                                    m_ReaderInstance.onReaderDataReceived += m_ReaderInstance_onReaderDataReceived;

                                    //Set reader connection flag to true
                                    m_IsRFIDReaderConnected = true;
                                }
                            }
                        }
                        //Houston we have a problem...
                        catch (Exception)
                        {

                            //Set reader connection flag to false
                            m_IsRFIDReaderConnected = false;

                            //Signal subscribers that connection is down
                            this.OnConnectionChanged.Invoke(this, new ReaderEventArgs(false));

                            //If reader instance still exists
                            if (m_ReaderInstance != null)
                            {
                                //Close the connection
                                m_ReaderInstance.CloseCommunication();

                                //Unsubscribe from reader data received event
                                m_ReaderInstance.onReaderDataReceived -= m_ReaderInstance_onReaderDataReceived;
                            }

                            //delete reader instance
                            m_ReaderInstance = null;
                        }
                    }
                    //Pause thread for 1 second
                    Thread.Sleep(1000);
                }
            })
            {
                IsBackground = true,
                Name = "InitReaderThread"
            };
            readerThread.Start();
        }

        /// <summary>
        /// Event that is triggered when reader receives data
        /// </summary>
        /// <param name="data">Part of RFID code</param>
        private void m_ReaderInstance_onReaderDataReceived(string data)
        {
            Thread t = new Thread(() =>
            {
                //Add received data to the end of the buffer
                m_ReceiveBuffer += data;

                //TODO: Different handling of received data
                //If last character is new line set receive complete flag to true; we received the RFID
                m_IsReceiveComplete = (m_ReceiveBuffer[m_ReceiveBuffer.Length - 1] == '\n');

                //If receive is complete
                if (m_IsReceiveComplete)
                {
                    //Replace terminating character
                    string rfid = m_ReceiveBuffer;
                    rfid = rfid.Replace(Environment.NewLine, String.Empty);

                    //Clear the buffer
                    m_ReceiveBuffer = String.Empty;

                    //Set receive complete flag to false
                    m_IsReceiveComplete = false;

                    //Query database for the RFID code
                    OnDataReceived?.Invoke(this, new RfidEventArgs(rfid));
                }
            })
            {
                IsBackground = true,
                Name = "OnReceiveDataThread"
            };

            t.Start();
        } 

        #endregion
    }
}
