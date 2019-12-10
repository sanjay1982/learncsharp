using System.Windows;
using System.Windows.Controls;
using CalculatorLib.ViewModels;

namespace Calculator.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_LoadView(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.LoadView((e.OriginalSource as MenuItem)?.Header.ToString());
        }
    }
}