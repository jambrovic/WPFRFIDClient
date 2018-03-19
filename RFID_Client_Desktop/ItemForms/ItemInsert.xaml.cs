using RFIDClient.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Interaction logic for ItemInsert.xaml
    /// </summary>
    public partial class ItemInsert : Window
    {
        private Item _item;
        public ItemInsert()
        {
            InitializeComponent();
        }

        private async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _item = new Item();
            _item.Barcode = txtBarcode.Text;
            _item.RFIDCode = txtRFIDCode.Text;
            _item.Name = txtName.Text;
            _item.SecondaryCode = txtSecondaryCode.Text;
            _item.UnitPrice = decimal.Parse(txtUnitPrice.Text);

            try
            {
                //await RFID_Client_Data.ItemFactory.GetInstance().Insert(_item);
                await ItemRepositoryServiceFactory.GetService().Insert(EntityHelpers.GetItem(_item));
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Item Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            txtBarcode.Text = "";
            txtName.Text = "";
            txtRFIDCode.Text = "";
            txtSecondaryCode.Text = "";
            txtUnitPrice.Text = "";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
