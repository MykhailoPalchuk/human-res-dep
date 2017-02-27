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
    /// Логика взаимодействия для AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        public AddProject()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Equals("") || costTextBox.Text.Equals(""))
            {
                var result = MessageBox.Show("Fill all lines to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    nameTextBox.BorderBrush = Brushes.Red;
                    costTextBox.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                try
                {
                    int cost = int.Parse(costTextBox.Text);
                    ProjectController projectController = new ProjectController();
                    projectController.AddProject(nameTextBox.Text, cost);
                    MessageBox.Show("Project added successfully!", "Success");
                    Close();
                }
                catch 
                {
                    var result = MessageBox.Show("Enter correct cost of new project", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    if (result == MessageBoxResult.OK)
                    {
                        costTextBox.BorderBrush = Brushes.Red;
                    }
                }
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
