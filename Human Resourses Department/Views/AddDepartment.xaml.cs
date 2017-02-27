using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Controllers;

namespace Views
{
    /// <summary>
    /// Логика взаимодействия для AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        public AddDepartment()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Equals(""))
            {
                MessageBox.Show("Fill all lines to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                nameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                DepartmentController departmentController = new DepartmentController();
                departmentController.AddDepartment(nameTextBox.Text);
                MessageBox.Show("Department added successfully!", "Success");
                TableWindow parent = Owner as TableWindow;
                parent.RefreshTable();
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Owner = null;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OK_Click(new object(), new RoutedEventArgs());
        }
    }
}
