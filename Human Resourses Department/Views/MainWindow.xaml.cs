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
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.CanMinimize;
        }

        //Open table window with workers
        private void Workers_Click(object sender, RoutedEventArgs e)
        {
            TableWindow workersWindow = new TableWindow("workers");
            workersWindow.Show();
            Close();
        }

        //Open table window with departents
        private void Departments_Click(object sender, RoutedEventArgs e)
        {
            TableWindow workersWindow = new TableWindow("departments");
            workersWindow.Show();
            Close();
        }

        //Open table window with positions
        private void Positions_Click(object sender, RoutedEventArgs e)
        {
            TableWindow workersWindow = new TableWindow("positions");
            workersWindow.Show();
            Close();
        }
    }
}
