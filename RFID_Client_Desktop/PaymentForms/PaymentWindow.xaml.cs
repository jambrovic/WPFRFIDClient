//using MongoDB.Bson;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace RFIDClient.Desktop
//{
//    /// <summary>
//    /// Interaction logic for PaymentWindow.xaml
//    /// </summary>
//    public partial class PaymentWindow : Window
//    {
//        private ReceiptViewModel _receiptVM;
//        private List<ReceiptPayment> previousPayments;
//        public PaymentWindow(ReceiptViewModel receipt)
//        {
//            InitializeComponent();
//            _receiptVM = receipt;
//            previousPayments = _receiptVM.Payments;
//            btnPaymentCash.IsChecked = true;
//            DisableCreditCards();
//        }

//        private void btnPaymentCash_Click(object sender, RoutedEventArgs e)
//        {
//            if (btnPaymentCash.IsChecked == true)
//            {
//                DisableCreditCards();
//            }
//            else
//            {
//                EnableCreditCards();
//            }
//        }

//        private void DisableCreditCards()
//        {
//            btnPaymentCards.IsChecked = false;
//            btnPaymentCek.IsEnabled = false;
//            btnPaymentDiners.IsEnabled = false;
//            btnPaymentMaestro.IsEnabled = false;
//            btnPaymentMaster.IsEnabled = false;
//            btnPaymentVisa.IsEnabled = false;
//            btnPaymentCek.IsEnabled = false;
//            btnPaymentAmex.IsEnabled = false;
//        }

//        private void EnableCreditCards()
//        {
//            btnPaymentCards.IsChecked = true;
//            btnPaymentCek.IsEnabled = true;
//            btnPaymentDiners.IsEnabled = true;
//            btnPaymentMaestro.IsEnabled = true;
//            btnPaymentMaster.IsEnabled = true;
//            btnPaymentVisa.IsEnabled = true;
//            btnPaymentCek.IsEnabled = true;
//            btnPaymentAmex.IsEnabled = true;
//        }

//        private void btnPaymentCards_Click(object sender, RoutedEventArgs e)
//        {
//            if (btnPaymentCards.IsChecked == true)
//            {
//                EnableCreditCards();
//            }
//            else
//            {
//                DisableCreditCards();
//            }
//        }

//        private void btnPaymentAmex_Click(object sender, RoutedEventArgs e)
//        {
//            InsertPayment(PaymentType.AMERICAN_EXPRESS, _receiptVM.Total, _receiptVM);
//        }

//        private void InsertPayment(PaymentType paymentType, decimal amount, ReceiptViewModel receipt)
//        {
//            decimal amountToPay = receipt.RestForPayment;

//            //If amountToPay equals amount then we will erase all existing payments and insert new one
//            if (amountToPay == amount)
//            {
//                switch (paymentType)
//                {
//                    case PaymentType.CASH:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id=ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.CASH),
//                                Name = "Novcanice"
//                            });
//                        }
//                        break;
//                    case PaymentType.CREDIT_CARD:
//                        break;
//                    case PaymentType.AMERICAN_EXPRESS:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id = ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.AMERICAN_EXPRESS),
//                                Name = "American Express"
//                            });
//                        }
//                        break;
//                    case PaymentType.VISA:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id = ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.VISA),
//                                Name = "Visa"
//                            });
//                        }
//                        break;
//                    case PaymentType.VISA_ELECTRON:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id = ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.VISA_ELECTRON),
//                                Name = "American Express"
//                            });
//                        }
//                        break;
//                    case PaymentType.MASTERCARD:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id = ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.MASTERCARD),
//                                Name = "MasterCard"
//                            });
//                        }
//                        break;
//                    case PaymentType.MAESTRO:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id = ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.MAESTRO),
//                                Name = "Maestro"
//                            });
//                        }
//                        break;
//                    case PaymentType.DINERS:
//                        {
//                            receipt.Payments.Clear();
//                            receipt.Payments.Add(new ReceiptPayment
//                            {
//                                Id = ObjectId.GenerateNewId().ToString(),
//                                Amount = amount,
//                                Code = Enum.GetName(typeof(PaymentType), PaymentType.DINERS),
//                                Name = "Diners"
//                            });
//                        }
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }

//        private void btnPaymentMaster_Click(object sender, RoutedEventArgs e)
//        {
//            InsertPayment(PaymentType.MASTERCARD, _receiptVM.Total, _receiptVM);

//        }

//        private void btnPaymentDiners_Click(object sender, RoutedEventArgs e)
//        {
//            InsertPayment(PaymentType.DINERS, _receiptVM.Total, _receiptVM);

//        }

//        private void btnPaymentVisa_Click(object sender, RoutedEventArgs e)
//        {
//            InsertPayment(PaymentType.VISA, _receiptVM.Total, _receiptVM);

//        }

//        private void btnPaymentMaestro_Click(object sender, RoutedEventArgs e)
//        {
//            InsertPayment(PaymentType.MAESTRO, _receiptVM.Total, _receiptVM);

//        }

//        private void btnPaymentCek_Click(object sender, RoutedEventArgs e)
//        {
//            InsertPayment(PaymentType.CEK, _receiptVM.Total, _receiptVM);

//        }

//        private void btnCancel_Click(object sender, RoutedEventArgs e)
//        {
//            //Return previous payments if existed
//            _receiptVM.Payments = previousPayments;

//            Close();
//        }

//        private void btnOK_Click(object sender, RoutedEventArgs e)
//        {
//            if (btnPaymentCash.IsChecked == true)
//            {
//                InsertPayment(PaymentType.CASH, _receiptVM.Total, _receiptVM);
//            }
//            _receiptVM.FinishReceipt();
//            Close();
//        }
//    }
//}
