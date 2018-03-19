using System.Windows;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Interaction logic for Receipt.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new WindowViewModel(this);
        }
    }
}
