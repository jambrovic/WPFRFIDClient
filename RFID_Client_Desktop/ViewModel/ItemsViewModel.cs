using RFIDClient.Desktop.Core;
using RFIDClient.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RFIDClient.Desktop
{
    public class ItemsViewModel : BaseViewModel
    {

        private ObservableCollection<ItemViewModel> m_Items;
        public ObservableCollection<ItemViewModel> Items { get; set; }
        public ICommand CloseCommand { get; set; }

        public ItemsViewModel()
        {

            CloseCommand = new RelayParameterizedCommand((parameter) => PageClose(parameter));

            m_Items = new ObservableCollection<ItemViewModel>();

            Task.Run(GetAllItems);
        }

        private void PageClose(object parameter)
        {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Receipt);
        }

        private async Task GetAllItems()
        {
            m_Items = EntityHelpers.GetObservableItems(await ItemRepositoryServiceFactory.GetService().SelectAll());
            Items = m_Items;
        }
    }
}
