using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        private bool change;
        private int id;

        public AddDepartment(bool change, int id)
        {
            this.change = change;
            this.id = id;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            if (change)
            {
                DepartmentController dc = new DepartmentController();
                var dep = dc.GetDepartmentInfo(id);
                string name;
                dep.TryGetValue("name", out name);
                nameTextBox.Text = name;
            }
        }
        
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Equals("") || !Input.IsName(nameTextBox.Text))
            {
                MessageBox.Show("Enter correct data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                label1.Content = "Start with capital letter";
                nameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                DepartmentController departmentController = new DepartmentController();
                if (change)
                {
                    departmentController.ChangeName(id, nameTextBox.Text);
                    MessageBox.Show("Department data changed successfully!", "Success");
                }
                else
                {
                    departmentController.AddDepartment(nameTextBox.Text);
                    MessageBox.Show("Department added successfully!", "Success");
                }
                TableWindow parent = Owner as TableWindow;
                if (parent != null)
                    parent.RefreshTable(sender, e);
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
