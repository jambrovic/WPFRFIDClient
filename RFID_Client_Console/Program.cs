using RFID_Client_Data;
using RFID_Client_Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFID_Client_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            DALReceipt r = new DALReceipt();
            r.DateCreated = DateTime.Now;
            r.JIR = "jir_jir_jir_jir_jir_jir_jir_jir";
            r.ZKI = "zki_zki_zki_zki_zki_zki_zki_zki";
            r.Items = new List<DALReceiptTransaction>
            {
                new DALReceiptTransaction
                {
                    Barcode="1111111111111",
                    Name="Item A",
                    SecondaryCode="A_111",
                    RFIDCode="1234567890",
                    Quantity=1,
                    UnitPrice=12.56M
                },
                new DALReceiptTransaction
                {
                    Barcode="2222222222222",
                    Name="Item B",
                    SecondaryCode="B_222",
                    RFIDCode="1234567891",
                    Quantity=1,
                    UnitPrice=15.56M
                }
            };
            r.Payments = new List<DALPayment>
            {
                new DALPayment
                {
                    Code="AMEX",
                    Name="American Express",
                    Amount=28.12M
                }
            };

            await ReceiptFactory.GetInstance().Insert(r);

            Console.WriteLine( await ReceiptFactory.GetInstance().Select("1111111111111"));

        }
    }
}
