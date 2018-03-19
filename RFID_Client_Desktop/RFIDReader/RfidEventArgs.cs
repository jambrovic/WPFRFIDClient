using System;
namespace RFIDClient.Desktop
{
    /// <summary>
    /// RFID module event event arguments
    /// </summary>
    public class RfidEventArgs : EventArgs
    {
        #region Private Members

        /// <summary>
        /// RFID code
        /// </summary>
        private string mRfid;

        #endregion

        #region Public Properties

        /// <summary>
        /// RFID code
        /// </summary>
        public string RFID => mRfid;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor. Please use parametered constructor.
        /// </summary>
        private RfidEventArgs() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="rfid"></param>
        public RfidEventArgs(string rfid)
        {
            mRfid = rfid;
        } 
        #endregion
    }
}
