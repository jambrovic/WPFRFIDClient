namespace RFIDClient.Arduino
{
    /// <summary>
    /// Factory class for IReader instance
    /// </summary>
    public sealed class ReaderFactory
    {
        #region Private Members

        private static IReader m_Instance;

        #endregion

        #region Constructor

        private ReaderFactory() { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Configure port and get reader instance
        /// </summary>
        /// <param name="portName">Port name</param>
        /// <param name="baudRate">Baud rate</param>
        /// <param name="dtrEnable">DTR Enable</param>
        /// <returns></returns>
        public static IReader GetInstance(string portName, int baudRate, bool dtrEnable)
        {
            if (m_Instance == null)
            {
                m_Instance = new Reader(portName, baudRate, dtrEnable);
            }
            return m_Instance;
        }
        
        #endregion
    }
}
