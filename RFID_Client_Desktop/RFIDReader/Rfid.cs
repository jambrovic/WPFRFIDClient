using RFIDClient.Arduino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFIDClient.Desktop
{
    public class Rfid
    {
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

        public event EventHandler<RfidEventArgs> OnDataReceived;

        public Rfid()
        {
            InitReader();
        }

        public void Disconnect()
        {
            if (m_ReaderInstance != null)
            {
                m_ReaderInstance.onReaderDataReceived -= m_ReaderInstance_onReaderDataReceived;
            }
        }

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
                            m_ReaderInstance.OpenCommunication();

                            //Init the receive buffer
                            m_ReceiveBuffer = String.Empty;

                            //Port is opened, 
                            if (m_ReaderInstance != null)
                            {
                                m_ReaderInstance.onReaderDataReceived += m_ReaderInstance_onReaderDataReceived;
                                m_IsRFIDReaderConnected = true;
                            }

                        }
                        catch (Exception)
                        {

                            m_IsRFIDReaderConnected = false;
                            if (m_ReaderInstance != null)
                            {
                                m_ReaderInstance.CloseCommunication();
                                m_ReaderInstance.onReaderDataReceived -= m_ReaderInstance_onReaderDataReceived;
                            }
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
    }
}
