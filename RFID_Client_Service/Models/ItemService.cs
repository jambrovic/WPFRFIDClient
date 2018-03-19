using System;
using System.Collections.Generic;
using System.Text;

namespace RFIDClient.Service
{
    public class ItemService
    {
        public string Id { get; set; }

        public string Barcode { get; set; }

        public string RFIDCode { get; set; }

        public string SecondaryCode { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
