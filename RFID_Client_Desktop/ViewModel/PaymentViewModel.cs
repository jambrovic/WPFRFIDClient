using RFIDClient.Desktop.Core;
using System;
using System.Windows.Input;

namespace RFIDClient.Desktop
{
    public class PaymentViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// ID of the payment
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Receipt view model
        /// </summary>
        public ReceiptViewModel Receipt { get; set; }

        /// <summary>
        /// Internal code of the payment
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Friendly name of the payment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Amount of the payment
        /// </summary>
        public decimal Amount { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add Payment to receipt
        /// </summary>
        /// <param name="amount">Amount to charge</param>
        public void AddPayment(object parameter, decimal amount = 0.0M)
        {
            Amount = amount;

            switch (parameter as PaymentType?)
            {
                case PaymentType.CASH:
                    AddPayment(PaymentType.CASH);
                    break;
                case PaymentType.CREDIT_CARD:
                    AddPayment(PaymentType.CREDIT_CARD);
                    break;
                case PaymentType.AMERICANEXPRESS:
                    AddPayment(PaymentType.AMERICANEXPRESS);
                    break;
                case PaymentType.VISA:
                    AddPayment(PaymentType.VISA);
                    break;
                case PaymentType.VISA_ELECTRON:
                    AddPayment(PaymentType.VISA_ELECTRON);
                    break;
                case PaymentType.MASTERCARD:
                    AddPayment(PaymentType.MASTERCARD);
                    break;
                case PaymentType.MAESTRO:
                    AddPayment(PaymentType.MAESTRO);
                    break;
                case PaymentType.DINERS:
                    AddPayment(PaymentType.DINERS);
                    break;
                case PaymentType.CEK:
                    AddPayment(PaymentType.CEK);
                    break;
                case PaymentType.OSTALO:
                    AddPayment(PaymentType.OSTALO);
                    break;
                case PaymentType.PAYPAL:
                    AddPayment(PaymentType.PAYPAL);
                    break;

                default:
                    AddPayment(PaymentType.CASH);
                    break;
            }
        } 
        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand PaymentAmericanExpressCommand { get; set; }
        public ICommand PaymentMasterCardCommand { get; set; }
        public ICommand PaymentDinersCommand { get; set; }
        public ICommand PaymentCashCommand { get; set; }
        public ICommand PaymentVisaCommand { get; set; }
        public ICommand PaymentPayPalCommand { get; set; }
        public ICommand PaymentMaestroCommand { get; set; } 
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public PaymentViewModel()
        {
            //m_ReceiptViewModel = new ReceiptViewModel();
            //m_ReceiptViewModel.Payments = new System.Collections.ObjectModel.ObservableCollection<PaymentViewModel>();

            CloseCommand = new RelayParameterizedCommand((parameter) => ClosePage(parameter));
            PaymentPayPalCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
            PaymentMaestroCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
            PaymentCashCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
            PaymentMasterCardCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
            PaymentAmericanExpressCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
            PaymentDinersCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
            PaymentVisaCommand = new RelayParameterizedCommand((parameter) => AddPayment(parameter));
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Closes payment page
        /// </summary>
        /// <param name="parameter"></param>
        private void ClosePage(object parameter)
        {
            IoC.Get<ApplicationViewModel>().PaymentPage = ApplicationPage.None;
        }

        /// <summary>
        /// Add payment
        /// </summary>
        /// <param name="paymentType">Type of the payment to add</param>
        private void AddPayment(PaymentType paymentType)
        {
            //Get current receipt model
            Receipt = ((PaymentViewModel)IoC.Application.CurrentPageViewModel).Receipt;

            //Set payment view model properties
            Name = paymentType.ToString();
            Code = Enum.GetName(typeof(PaymentType), paymentType);
            Id = ObjectIdFactory.GetObjectId().ToString();
            Amount = Receipt.Total;

            //Add payment to receipt view model
            Receipt.Payments.Add(this);

            //Close the payment page
            IoC.Get<ApplicationViewModel>().PaymentPage = ApplicationPage.None;

        } 
        #endregion
    }
}
