using Calculator.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculatorViewModel Model = new CalculatorViewModel();
        public MainWindow()
        {
            Model.Commands = new List<Command>
            {
                new Command
                {
                    Text="1",
                    Value="1"
                },
                new Command
                {
                    Text="2",
                    Value="2"
                },
                new Command
                {
                    Text="3",
                    Value="1"
                },
                new Command
                {
                    Text="4",
                    Value="1"
                },
            };
            InitializeComponent();
            DataContext = Model;
        }
    }
}
