using RFIDClient.Desktop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// View model for single receipt transaction
    /// </summary>
    public class TransactionViewModel : BaseViewModel
    {
        #region Private Members


        #endregion

        #region Public Properties

        public string Id { get; set; }

        public string RFIDCode { get; set; }

        public string SecondaryCode { get; set; }

        public DateTime Timestamp { get; set; }
        public TransactionViewModel()
        {
        }

        /// <summary>
        /// Ordinal number of the receipt transaction
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Item barcode
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Item quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Item unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Item discount percent for applying discount to receipt transaction
        /// </summary>
        public decimal DiscountPercent { get; set; }

        /// <summary>
        /// Total amount of receipt transaction
        /// </summary>
        public decimal Total
        {
            get
            {
                return Math.Round(Quantity * UnitPrice * ((100 - DiscountPercent) / 100), 2);
            }
        }

        public string FormattedOrdinal => string.Format("{0}.", Ordinal);

        #endregion
    }
}
