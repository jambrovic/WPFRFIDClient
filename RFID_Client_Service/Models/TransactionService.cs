using System;
using System.Collections.Generic;
using System.Text;

namespace RFIDClient.Service
{
    public class TransactionService
    {
        public string Id { get; set; }

        public string Barcode { get; set; }

        public string RFIDCode { get; set; }

        public string SecondaryCode { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal Total { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
