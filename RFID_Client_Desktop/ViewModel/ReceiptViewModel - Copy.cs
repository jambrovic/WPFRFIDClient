//using System;
//using System.Linq;
//using System.Collections.ObjectModel;
//using System.Windows;
//using RFIDClient.Service;
//using RFIDClient.Desktop.Core;

//namespace RFIDClient.Desktop
//{
//    /// <summary>
//    /// Receipt view model
//    /// </summary>
//    public class ReceiptViewModel : BaseViewModel
//    {

//        #region Private members
//        /// <summary>
//        /// Receipt total
//        /// </summary>
//        private decimal m_Total;

//        #endregion

//        #region Public Properties

//        /// <summary>
//        /// Id of the receipt
//        /// </summary>
//        public string Id { get; set; }

//        /// <summary>
//        /// Timestamp of the receipt creation
//        /// </summary>
//        public DateTime DateCreated { get; set; }

//        /// <summary>
//        /// Collection of transactions
//        /// </summary>
//        public ObservableCollection<TransactionViewModel> Transactions { get; set; }

//        /// <summary>
//        /// Collection of payments
//        /// </summary>
//        public ObservableCollection<PaymentViewModel> Payments { get; set; }

//        /// <summary>
//        /// Total quantity of all transactions
//        /// </summary>
//        public decimal QuantityTotal
//        {
//            get
//            {
//                decimal _total = 0;

//                if (Transactions != null)
//                {
//                    foreach (var item in Transactions)
//                    {
//                        _total += item.Quantity;
//                    }
//                }
//                return _total;
//            }
//        }

//        /// <summary>
//        /// Total amount of all transactions
//        /// </summary>
//        public decimal Total
//        {
//            get
//            {
//                m_Total = 0;
//                if (Transactions != null)
//                {
//                    foreach (var item in Transactions)
//                    {
//                        m_Total += item.Total;
//                    }
//                }
//                return m_Total;
//            }
//            set
//            {
//                m_Total = value;
//            }
//        }

//        /// <summary>
//        /// Amount to pay
//        /// </summary>
//        public decimal RestForPayment
//        {
//            get
//            {
//                return Total - Payments.Sum(p => p.Amount);
//            }
//        }

//        /// <summary>
//        /// Jedinstveni Identifikator Rračuna
//        /// </summary>
//        public string JIR { get; set; }

//        /// <summary>
//        /// Zaštitni Kod
//        /// </summary>
//        public string ZKI { get; set; }

//        /// <summary>
//        /// Timestamp when the receipt was finished
//        /// </summary>
//        public DateTime DateFinished { get; set; }


//        #endregion

//        #region Constructor

//        /// <summary>
//        /// Main constructor for ReceiptViewModel
//        /// </summary>
//        public ReceiptViewModel()
//        {
//            //Assign new ObjectId
//            this.Id = ObjectIdFactory.GetObjectIdString();

//            //Init TransactionViewModel collection
//            Transactions = new ObservableCollection<TransactionViewModel>();

//            //Init PaymentViewModel collection
//            Payments = new ObservableCollection<PaymentViewModel>();

//            //Set creation timestamp
//            this.DateCreated = DateTime.Now.ToLocalTime();

//            //Subscribe to Observable Collection changed event
//            this.Transactions.CollectionChanged += Transactions_CollectionChanged;
//        }

//        #endregion
        
//        #region Public Methods

//        /// <summary>
//        /// Check for existance and add receipt transaction
//        /// </summary>
//        /// <param name="transaction">transaction to insert</param>
//        public void AddItem(TransactionViewModel transaction)
//        {
//            //Add transaction if does not exists
//            if (!TransactionExists(transaction))
//            {
//                Transactions.Add(transaction);
//                return;
//            }

//            //Instatianate transactions if collection is null
//            if (Transactions == null)
//            {
//                Transactions = new ObservableCollection<TransactionViewModel>();
//            }

//            //Duplicate of first if?
//            if (!Transactions.Select(i => i.Barcode.Equals(transaction.Barcode)).FirstOrDefault())
//            {
//                Transactions.Add(transaction);
//            }

//            //Refresh();
//        }

//        /// <summary>
//        /// Refresh receipt footer
//        /// </summary>
//        public void Refresh()
//        {
//            OnPropertyChanged(nameof(Total));
//            OnPropertyChanged(nameof(QuantityTotal));
//            OnPropertyChanged(nameof(Payments));
//        }

//        #endregion

//        #region Private Methods


//        /// <summary>
//        /// Transactions collection changed event
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void Transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
//        {
//            //Refresh receipt footer 
//            Refresh();
//        }

//        /// <summary>
//        /// Check if item already exists in transactions collection
//        /// </summary>
//        /// <param name="transaction">Transaction to check existence</param>
//        /// <returns></returns>
//        private bool TransactionExists(TransactionViewModel transaction)
//        {
//            foreach (TransactionViewModel row in Transactions)
//            {
//                if (Transactions.Count > 0 && row.Barcode.Equals(transaction.Barcode))
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// Receipt finish asyncronous method
//        /// </summary>
//        internal async void FinishReceipt()
//        {
//            try
//            {
//                //Set receipt finish timestamp
//                this.DateFinished = DateTime.Now.ToLocalTime();

//                //Insert the receipt
//                await ReceiptRepositoryServiceFactory.GetService().Insert(EntityHelpers.GetReceipt(this));

//                //Clear receipt transactions
//                Transactions = new ObservableCollection<TransactionViewModel>();

//                //Clear receipt payments
//                Payments = new ObservableCollection<PaymentViewModel>();

//                //Give receipte a new Id
//                Id = ObjectIdFactory.GetObjectIdString();

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Receipt Finish", MessageBoxButton.OK, MessageBoxImage.Error);
//            }

//        }

//        #endregion
//    }
//}
