using System;
using System.Collections.Generic;
using System.Text;

namespace RFIDClient.Service
{
    public class PaymentService
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
