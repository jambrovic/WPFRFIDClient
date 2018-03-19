using RFIDClient.Desktop.Core;
using RFIDClient.Service;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Item viewmodel
    /// </summary>
    public class ItemViewModel : BaseViewModel
    {
        private readonly Rfid m_Rfid;

        #region Commands

        /// <summary>
        /// Close page command
        /// </summary>
        public ICommand CloseCommand { get; set; }
        /// <summary>
        /// Insert new item command
        /// </summary>
        public ICommand InsertCommand { get; set; }
        #endregion

        #region Public Properties

        /// <summary>
        /// Id of the item
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Barcode of the item
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// RFID code of the item
        /// </summary>
        public string RFIDCode { get; set; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Secondary/internal code of the item
        /// </summary>
        public string SecondaryCode { get; set; }

        /// <summary>
        /// Unit price of the item
        /// </summary>
        public decimal UnitPrice { get; set; }

        #endregion

        public ItemViewModel()
        {
            CloseCommand = new RelayParameterizedCommand((parameter) => ClosePage(parameter));
            InsertCommand = new RelayParameterizedCommand(async (parameter) => await InsertItem(parameter));

            m_Rfid = RfidReader.Instance;
            m_Rfid.OnDataReceived += M_Rfid_OnDataReceived;
        }

        private void M_Rfid_OnDataReceived(object sender, EventArgs e)
        {
            RFIDCode = (e as RfidEventArgs).RFID;
        }

        private async Task InsertItem(object parameter)
        {
            if (parameter is null)
                return;

            try
            {
                var item = parameter as IItem;
                ItemViewModel itemVM = new ItemViewModel
                {
                    Barcode = item.Barcode,
                    Id = item.Id,
                    Name = item.ItemName,
                    RFIDCode = item.RFIDCode,
                    SecondaryCode = item.SecondaryCode,
                    UnitPrice = item.UnitPrice
                };
                await ItemRepositoryServiceFactory.GetService().Insert(EntityHelpers.GetItem(itemVM));
                item.ResetForm();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }
        }

        private void ClosePage(object parameter)
        {
            var item = parameter as IItem;
            
            //Unsubscribe from RFID reader
            item.UnsubscribeFromRfid();

            //Change page
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Receipt);
        }
    }
}
