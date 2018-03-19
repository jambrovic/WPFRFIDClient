using RFIDClient.Desktop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop
{
    public class Receipt
    {
        public string Id { get; set; }

        public DateTime DateCreated { get; set; }

        public List<ReceiptTransaction> Items { get; set; }

        public List<ReceiptPayment> Payments { get; set; }

        private decimal _total;

        public decimal Total
        {
            get
            {
                _total = 0;
                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        _total += item.Total;
                    }
                }
                return _total;
            }
            set
            {
                _total = value;
            }
        }

        public string ZKI { get; set; }

        public string JIR { get; set; }

        public DateTime DateFinished { get; set; }

        public decimal QuantityTotal
        {
            get
            {
                decimal _total = 0;

                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        _total += item.Quantity;
                    }
                }
                return _total;
            }
        }
        public decimal UnitPriceTotal
        {
            get
            {
                decimal _total = 0;
                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        _total += item.Quantity * item.UnitPrice;
                    }

                }
                return _total;
            }
        }

        public Receipt()
        {
            this.DateCreated = DateTime.Now.ToLocalTime();
            this.Items = new List<ReceiptTransaction>();
            this.Payments = new List<ReceiptPayment>();
            this.Id = ObjectIdFactory.GetObjectIdString();
        }
    }
}
