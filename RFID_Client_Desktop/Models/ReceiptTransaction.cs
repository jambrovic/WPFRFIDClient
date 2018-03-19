using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop
{
    public class ReceiptTransaction
    {
        public string Id { get; set; }

        public string Barcode { get; set; }

        public string RFIDCode { get; set; }

        public string SecondaryCode { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal Total
        {
            get
            {
                return Math.Round(Quantity * UnitPrice * ((100 - DiscountPercent) / 100), 2);
            }
        }

        public int Ordinal { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
