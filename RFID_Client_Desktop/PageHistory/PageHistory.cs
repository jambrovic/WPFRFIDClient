using RFIDClient.Desktop.Core;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Class for handling page history, handles only one step in the past
    /// </summary>
    public static class PageHistory
    {
        /// <summary>
        /// Current page
        /// </summary>
        public static BaseViewModel CurrentPage { get; set; }

        /// <summary>
        /// Next page
        /// </summary>
        public static BaseViewModel NextPage { get; set; }

        /// <summary>
        /// Previous page
        /// </summary>
        public static BaseViewModel PreviousPage => CurrentPage;
    }
}
