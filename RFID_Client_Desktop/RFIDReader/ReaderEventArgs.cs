using System;
namespace RFIDClient.Desktop
{
    /// <summary>
    /// RFID module connection event arguments
    /// </summary>
    public class ReaderEventArgs : EventArgs
    {
        #region Private Members

        /// <summary>
        /// RFID connection state
        /// </summary>
        private bool mIsConnected;

        #endregion

        #region Public Properties

        /// <summary>
        /// Reader connection state
        /// </summary>
        public bool IsConnected => mIsConnected;


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor. Please use parametered constructor.
        /// </summary>
        private ReaderEventArgs() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="rfid"></param>
        public ReaderEventArgs(bool isConnected)
        {
            mIsConnected = isConnected;
        } 
        #endregion
    }
}
