using RFIDClient.Desktop.Core;
using System;
using System.Windows;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Interaction logic for NewItemPage.xaml
    /// </summary>
    public partial class NewItemPage : BasePage<ItemViewModel>, IItem
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NewItemPage()
        {
            InitializeComponent();
            RfidReader.Instance.OnDataReceived += Rfid_OnDataReceived;
        }

        /// <summary>
        /// Event method that triggers when RFID send scanned RFID data
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e"><see cref="RfidEventArgs"/> parameter</param>
        private void Rfid_OnDataReceived(object sender, RfidEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    txtRFIDCode.Text = e.RFID;
                });
            }
            catch (Exception ex)
            {
                //TODO: Log to some logger class
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public string Id => ObjectIdFactory.GetObjectId().ToString();

        public string Barcode => txtBarcode.Text;

        public string RFIDCode => txtRFIDCode.Text;

        public string SecondaryCode => txtSecondaryCode.Text;

        public decimal UnitPrice => decimal.Parse(txtUnitPrice.Text);

        public string ItemName => txtName.Text;

        private void ResetForm()
        {
            txtBarcode.Text = "";
            txtName.Text = "";
            txtRFIDCode.Text = "";
            txtSecondaryCode.Text = "";
            txtUnitPrice.Text = "";
        }

        public void UnsubscribeFromRfid()
        {
            if(RfidReader.Instance != null)
            {
                RfidReader.Instance.OnDataReceived -= Rfid_OnDataReceived;
            }
        }

        void IItem.ResetForm()
        {
            this.ResetForm();
        }
    }
}
