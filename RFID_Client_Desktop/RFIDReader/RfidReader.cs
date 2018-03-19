namespace RFIDClient.Desktop
{
    public sealed class RfidReader
    {
        #region Private Members

        private static readonly Rfid m_Instance = new Rfid();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        private RfidReader() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Instance of the Rfid class
        /// </summary>
        public static Rfid Instance => m_Instance;
        #endregion
    }
}
