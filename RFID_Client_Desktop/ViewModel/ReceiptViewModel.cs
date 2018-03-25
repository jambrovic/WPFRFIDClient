using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using RFIDClient.Service;
using RFIDClient.Desktop.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using RFIDClient.Desktop.Utils;
using RFIDClient.Helpers;
using System.Windows.Input;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Receipt view model
    /// </summary>
    public class ReceiptViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// Receipt total
        /// </summary>
        private decimal m_Total;

        /// <summary>
        /// HashSet collection of scanned items identified by RFID code.
        /// Prevents inserting same RFID code multiple times.
        /// </summary>
        private HashSet<string> m_ReceiptScannedItemsBuffer;

        /// <summary>
        /// Receipt transaction ordinal number
        /// </summary>
        private int m_ReceiptTransactionOrdinal = 1;

        private Rfid m_Rfid = RfidReader.Instance;

        #endregion

        #region Public Properties

        /// <summary>
        /// Id of the receipt
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Timestamp of the receipt creation
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Collection of transactions
        /// </summary>
        public ObservableCollection<TransactionViewModel> Transactions { get; set; }

        /// <summary>
        /// Collection of payments
        /// </summary>
        public ObservableCollection<PaymentViewModel> Payments { get; set; }

        /// <summary>
        /// Total quantity of all transactions
        /// </summary>
        public decimal QuantityTotal
        {
            get
            {
                decimal _total = 0;

                if (Transactions != null)
                {
                    foreach (var item in Transactions)
                    {
                        _total += item.Quantity;
                    }
                }
                return _total;
            }
        }

        /// <summary>
        /// Total amount of all transactions
        /// </summary>
        public decimal Total
        {
            get
            {
                m_Total = 0;
                if (Transactions != null)
                {
                    foreach (var item in Transactions)
                    {
                        m_Total += item.Total;
                    }
                }
                return m_Total;
            }
            set
            {
                m_Total = value;
            }
        }

        /// <summary>
        /// Amount to pay
        /// </summary>
        public decimal RestForPayment
        {
            get
            {
                return Total - Payments.Sum(p => p.Amount);
            }
        }

        /// <summary>
        /// Jedinstveni Identifikator Rračuna
        /// </summary>
        public string JIR { get; set; }

        /// <summary>
        /// Zaštitni Kod
        /// </summary>
        public string ZKI { get; set; }

        /// <summary>
        /// Timestamp when the receipt was finished
        /// </summary>
        public DateTime DateFinished { get; set; }

        //TODO: Read it from DB
        /// <summary>
        /// Logged user
        /// </summary>
        public string User { get; set; } = "Korisnik: Mario Jambrović";

        /// <summary>
        /// Current date and time
        /// </summary>
        public string Clock { get; set; }

        /// <summary>
        /// Connection state
        /// </summary>
        public string ConnectionState { get; set; }


        #endregion

        #region Commands

        /// <summary>
        /// Application exit command
        /// </summary>
        public ICommand ApplicationExitCommand { get; set; }

        /// <summary>
        /// User log out command
        /// </summary>
        public ICommand UserLogoutCommand { get; set; }

        /// <summary>
        /// New user command
        /// </summary>
        public ICommand NewUserCommand { get; set; }

        /// <summary>
        /// New receipt command
        /// </summary>
        public ICommand NewReceiptCommand { get; set; }

        /// <summary>
        /// Day overview command
        /// </summary>
        public ICommand DayOverviewCommand { get; set; }

        /// <summary>
        /// New item command
        /// </summary>
        public ICommand NewItemCommand { get; set; }

        /// <summary>
        /// All items command
        /// </summary>
        public ICommand AllItemsCommand { get; set; }

        /// <summary>
        /// Application options command
        /// </summary>
        public ICommand ApplicationOptionsCommand { get; set; }

        /// <summary>
        /// Application about command
        /// </summary>
        public ICommand ApplicationAboutCommand { get; set; }

        /// <summary>
        /// Receipt checkout command
        /// </summary>
        public ICommand CheckoutCommand { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Main constructor for ReceiptViewModel
        /// </summary>
        public ReceiptViewModel()
        {
            
            //Create commands
            ApplicationExitCommand = new RelayParameterizedCommand((parameter) => ApplicationExit(parameter));
            UserLogoutCommand = new RelayParameterizedCommand((parameter) => UserLogout(parameter));
            NewUserCommand = new RelayParameterizedCommand((parameter) => NewUser(parameter));
            NewReceiptCommand = new RelayParameterizedCommand((parameter) => NewReceipt(parameter));
            DayOverviewCommand = new RelayParameterizedCommand((parameter) => DayOverview(parameter));
            NewItemCommand = new RelayParameterizedCommand((parameter) => NewItem(parameter));
            AllItemsCommand = new RelayParameterizedCommand((parameter) => GetAllItems(parameter));
            ApplicationOptionsCommand = new RelayParameterizedCommand((parameter) => ApplicationOptions(parameter));
            ApplicationAboutCommand = new RelayParameterizedCommand((parameter) => ApplicationAbout(parameter));
            CheckoutCommand = new RelayParameterizedCommand((parameter) => Checkout(parameter));

            
            //Subscription to events
            m_Rfid.OnDataReceived += Rfid_OnDataReceived;
            m_Rfid.OnConnectionChanged += Rfid_OnConnectionChanged;
            
            //Assign new ObjectId
            this.Id = ObjectIdFactory.GetObjectIdString();

            //Init TransactionViewModel collection
            Transactions = new ObservableCollection<TransactionViewModel>();

            //Init PaymentViewModel collection
            Payments = new ObservableCollection<PaymentViewModel>();

            //Init scanned items buffer
            m_ReceiptScannedItemsBuffer = new HashSet<string>();

            //Set creation timestamp
            this.DateCreated = DateTime.Now.ToLocalTime();

            //Subscribe to Observable Collection changed event
            this.Transactions.CollectionChanged += Transactions_CollectionChanged;

            //Check for page history
            if (PageHistory.CurrentPage != null)
            {
                //Get last opened receipt from history
                var parentReceipt = PageHistory.CurrentPage as ReceiptViewModel;

                //Assign values from receipt
                Id = parentReceipt.Id;
                DateCreated = parentReceipt.DateCreated;
                Transactions = parentReceipt.Transactions;
                Payments = parentReceipt.Payments;

                //Subscribe to Collection.Changed events
                Transactions.CollectionChanged += Transactions_CollectionChanged;

                //Get last transaction ordinal
                m_ReceiptTransactionOrdinal = m_ReceiptTransactionOrdinal == 1 ? 1 : Transactions.Max(t => t.Ordinal) + 1;

                //Reset page history
                PageHistory.CurrentPage = null;
            }

            //Set the live clock timer
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            //Set default currency
            Currency.SetCurrency(CurrencyName.KN);
        }

        private void Rfid_OnConnectionChanged(object sender, ReaderEventArgs e)
        {
            switch (e.IsConnected)
            {
                case true:
                    this.ConnectionState = "Connected";
                    break;
                case false:
                    this.ConnectionState = "Disconnected";
                    break;
                default:
                    this.ConnectionState = "Disconnected";
                    break;
            }
        }

        private void Rfid_OnDataReceived(object sender, EventArgs e)
        {
            string rfid = (e as RfidEventArgs).RFID;
            //Query database for the RFID code
            App.Current.Dispatcher.Invoke(async () => await AddReceiptTransaction(rfid));
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Clock = DateTime.Now.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void Checkout(object parameter)
        {
            //If there are transactions but no payments open payment page
            if (Payments != null && Payments.Count == 0 && Total > 0.0M)
            {
                PaymentViewModel paymentViewModel = new PaymentViewModel();
                paymentViewModel.Receipt = this;
                IoC.Get<ApplicationViewModel>().OpenPaymentPage(ApplicationPage.Payment, paymentViewModel);
            }
            //If there are payments but balance is still not equal open payment page
            else if (Total != Payments.Sum<PaymentViewModel>(p => p.Amount))
            {
                PaymentViewModel paymentViewModel = new PaymentViewModel();
                paymentViewModel.Receipt = this;
                IoC.Get<ApplicationViewModel>().OpenPaymentPage(ApplicationPage.Payment, paymentViewModel);
            }
            //If balance is 0 do nothing
            else if (Total == 0)
            {
                return;
            }
            //Finish the receipt
            else
            {
                FinishReceipt();
            }
        }


        private void DayOverview(object parameter)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }

        private void NewUser(object parameter)
        {
            PageHistory.CurrentPage = this;
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.NewUser);
        }

        private void UserLogout(object parameter)
        {
            
            switch (MessageBox.Show("Odjaviti korisnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning)) 
            {
                case MessageBoxResult.No:
                    {
                        //do nothing
                    }
                    break;
                case MessageBoxResult.Yes:
                    {
                        //TODO: Create user handling class
                        //TODO: Log Out current user
                        PageHistory.CurrentPage = this;
                        IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Login);
                    }
                    break;
                default:
                    {
                        //Do nothing
                    }
                    break;
            }
        }


        private void ApplicationAbout(object parameter)
        {
            //TODO: not yet implemented
        }

        private void ApplicationOptions(object parameter)
        {
            //TODO: not yet implemented
        }

        /// <summary>
        /// Opens page with all items
        /// </summary>
        /// <param name="parameter">ICommand parameter</param>
        private void GetAllItems(object parameter)
        {
            try
            {
                //Unsubscribe from RFID event
                if (m_Rfid!=null)
                {
                    m_Rfid.OnDataReceived -= Rfid_OnDataReceived;
                }

                //Save page datacontext
                PageHistory.CurrentPage = this;

                //Open new page
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.AllItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Items", MessageBoxButton.OK);
            }
        }

        private void NewItem(object parameter)
        {
            //Save the receipt state in history
            PageHistory.CurrentPage = this;

            //Remove subscription for rfid module data receive
            if (m_Rfid != null)
            {
                m_Rfid.OnDataReceived -= Rfid_OnDataReceived;
            }

            //Open new page
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.NewItem);
        }

        private void NewReceipt(object parameter)
        {
            FinishReceipt();
            m_ReceiptScannedItemsBuffer.Clear();
        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="parameter"></param>
        private void ApplicationExit(object parameter)
        {
            try
            {
                if (m_Rfid != null)
                {
                    m_Rfid.OnDataReceived -= Rfid_OnDataReceived;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            Application.Current.MainWindow.Close();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check for existence and add receipt transaction
        /// </summary>
        /// <param name="transaction">transaction to insert</param>
        public void AddItem(TransactionViewModel transaction)
        {
            //Add transaction if does not exists
            if (!TransactionExists(transaction))
            {
                Transactions.Add(transaction);
                return;
            }

            //Instatianate transactions if collection is null
            if (Transactions == null)
            {
                Transactions = new ObservableCollection<TransactionViewModel>();
            }

            //Possible duplicate of the first if...
            if (!Transactions.Select(i => i.Barcode.Equals(transaction.Barcode)).FirstOrDefault())
            {
                Transactions.Add(transaction);
            }
        }

        /// <summary>
        /// Refresh receipt footer
        /// </summary>
        public void Refresh()
        {
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(QuantityTotal));
            OnPropertyChanged(nameof(Payments));
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Transactions collection changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Refresh receipt footer 
            Refresh();
        }

        /// <summary>
        /// Check if item already exists in transactions collection
        /// </summary>
        /// <param name="transaction">Transaction to check existence</param>
        /// <returns></returns>
        private bool TransactionExists(TransactionViewModel transaction)
        {
            foreach (TransactionViewModel row in Transactions)
            {
                if (Transactions.Count > 0 && row.Barcode.Equals(transaction.Barcode))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Receipt finish asyncronous method
        /// </summary>
        internal async void FinishReceipt()
        {
            try
            {
                //Set receipt finish timestamp
                this.DateFinished = DateTime.Now.ToLocalTime();

                //Insert the receipt
                await ReceiptRepositoryServiceFactory.Instance.Insert(EntityHelpers.GetReceipt(this));

                //Create new instance of receipt transactions
                Transactions = new ObservableCollection<TransactionViewModel>();

                //Subscribe to transactions events
                Transactions.CollectionChanged += Transactions_CollectionChanged;

                //Clear receipt payments
                Payments = new ObservableCollection<PaymentViewModel>();

                //Give receipte a new Id
                Id = ObjectIdFactory.GetObjectIdString();

                //Reset the items buffer
                m_ReceiptScannedItemsBuffer = new HashSet<string>();

                //Reset ordinal number to 1
                m_ReceiptTransactionOrdinal = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Receipt Finish", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Queries the repository for the item and adds the receipt transaction
        /// </summary>
        /// <param name="rfid">RFID code</param>
        /// <returns></returns>
        private async Task AddReceiptTransaction(string rfid)
        {
            try
            {
                //Item is scanned for the first time
                if (!m_ReceiptScannedItemsBuffer.Contains(rfid))
                {
                    //Insert rfid code into buffer
                    m_ReceiptScannedItemsBuffer.Add(rfid);

                    Item queriedItem;

                    using (new WaitCursor())
                    {
                        //Get item from the database
                        queriedItem = EntityHelpers.GetItem(await ItemRepositoryServiceFactory.GetService().Select(rfid));

                        if (queriedItem != null)
                        {

                            TransactionViewModel transaction = new TransactionViewModel
                            {
                                Ordinal = m_ReceiptTransactionOrdinal,
                                Barcode = queriedItem.Barcode,
                                Name = queriedItem.Name,
                                SecondaryCode = queriedItem.SecondaryCode,
                                RFIDCode = queriedItem.RFIDCode,
                                Quantity = 1,
                                UnitPrice = queriedItem.UnitPrice,
                                Id = ObjectIdFactory.GetObjectIdString(),
                                Timestamp = DateTime.Now.ToLocalTime()
                            };

                            //Add transaction
                            AddItem(transaction);
                            //Transactions.Add(transaction);

                            //Increase transaction ordinal number
                            m_ReceiptTransactionOrdinal++;
                        }
                        else
                        {
                            //throw new Exception(String.Format("Item with RFID {0} does not exists!", rfid));
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                //Hide the message for now...
                //MessageBox.Show(String.Format("Item not found.{0}Please insert the item into the system!{1}RFID code: {2}", Environment.NewLine, Environment.NewLine, rfid), "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //Remove the item from buffer so it can be scanned again
                m_ReceiptScannedItemsBuffer.Remove(rfid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClientException.GetFormatedException(ex), rfid);
            }
        }

        /// <summary>
        /// Add dummy receipt transaction
        /// </summary>
        private void AddNewDummyItem()
        {
            try
            {
                TransactionViewModel transaction = new TransactionViewModel
                {
                    Ordinal = m_ReceiptTransactionOrdinal,
                    Barcode = "33333333333",
                    Name = "Item c",
                    SecondaryCode = "C_333",
                    RFIDCode = "12345678912",
                    Quantity = 3,
                    UnitPrice = 36.56M,
                    Id = ObjectIdFactory.GetObjectIdString()
                };

                AddItem(transaction);

                m_ReceiptTransactionOrdinal++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ClientException.GetFormatedException(ex), "Error");
            }

        }

        #endregion
    }
}


