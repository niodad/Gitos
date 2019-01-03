using System.Windows;

namespace Gitos.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowInvoiceForm(object sender, RoutedEventArgs e)
        {
            new CreateInvoice().ShowDialog();

        }

    }
}