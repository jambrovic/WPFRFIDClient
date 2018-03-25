namespace RFIDClient.Arduino
{

    public delegate void ReaderEventHandler(string data);
    
    /// <summary>
    /// Interface for basic RFID functionality
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Event that triggers when data is received
        /// </summary>
        event ReaderEventHandler onReaderDataReceived;

        /// <summary>
        /// Event that triggers when connection is changed
        /// </summary>
        //event ReaderConnectionEventHandler onConnectionChanged;

        /// <summary>
        /// Instance of the RFID reader
        /// </summary>
        /// <param name="portName">Name of the port reader is connected to</param>
        /// <param name="baudRate">Speed of communication channel</param>
        /// <param name="dtrEnable">Data Terminal Ready (DTR) signal during serial communication</param>
        /// <returns></returns>
        IReader GetReader(string portName, int baudRate, bool dtrEnable);

        /// <summary>
        /// Open communication channel with reader
        /// </summary>
        bool OpenCommunication();

        /// <summary>
        /// Close communication channel with reader
        /// </summary>
        void CloseCommunication();

        /// <summary>
        /// Check if communication channel exists
        /// </summary>
        /// <returns></returns>
        bool IsCommunicationOpen();
    }
}
