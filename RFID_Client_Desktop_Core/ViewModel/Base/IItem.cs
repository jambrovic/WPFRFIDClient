using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop.Core
{
    public interface IItem
    {
        /// <summary>
        /// Unsubscride from RFID reader
        /// </summary>
        void UnsubscribeFromRfid();

        /// <summary>
        /// Reset New Item form
        /// </summary>
        void ResetForm();

        /// <summary>
        /// Id of the item
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Barcode of the item
        /// </summary>
        string Barcode { get; }

        /// <summary>
        /// RFID code of the item
        /// </summary>
        string RFIDCode { get; }

        /// <summary>
        /// Name of the item
        /// </summary>
        string ItemName { get; }

        /// <summary>
        /// Secondary/internal code of the item
        /// </summary>
        string SecondaryCode { get; }

        /// <summary>
        /// Unit price of the item
        /// </summary>
        decimal UnitPrice { get; }
        
    }
}
