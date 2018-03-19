using System;
using System.Collections.Generic;
using System.Text;

namespace RFIDClient.Service
{
    public class ReceiptService
    {
        public string Id { get; set; }

        public DateTime DateCreated { get; set; }

        public List<TransactionService> Items { get; set; }

        public List<PaymentService> Payments { get; set; }

        public decimal Total { get; set; }

        public string ZKI { get; set; }

        public string JIR { get; set; }

        public DateTime DateFinished { get; set; }
        
    }
}
