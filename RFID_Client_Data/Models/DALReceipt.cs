using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    public class DALReceipt
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("dateCreated")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateCreated { get; set; }

        [BsonElement("items")]
        public List<DALReceiptTransaction> Items { get; set; }

        [BsonElement("payments")]
        public List<DALPayment> Payments { get; set; }

        private decimal _total;

        [BsonElement("total")]
        public decimal Total
        {
            get
            {
                _total = 0;
                if(Items != null)
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

        [BsonElement("zki")]
        public string ZKI { get; set; }

        [BsonElement("jir")]
        public string JIR { get; set; }

        [BsonElement("dateFinished")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateFinished { get; set; }

        [BsonIgnore]
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
        [BsonIgnore]
        public decimal UnitPriceTotal
        {
            get
            {
                decimal _total = 0;
                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        _total += item.Quantity*item.UnitPrice;
                    }

                }
                return _total;
            }
        }
    }

}
