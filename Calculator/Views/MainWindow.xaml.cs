using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using CalculatorLib.ViewModels;
using log4net;

namespace Calculator.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Logger.Debug("Bye! Bye!");
            Close();
        }

        private void MenuItem_LoadView(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            var view = (e.OriginalSource as MenuItem)?.Header.ToString() ?? "";
            Logger.Debug("Loading view - " + view);
            viewModel?.LoadView(view);
            Logger.Debug("Loaded view - " + view);
        }
    }
}