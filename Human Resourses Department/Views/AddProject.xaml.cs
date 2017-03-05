using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        private int workerId;
        
        public AddProject()
        {
            workerId = 0;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        //Add new project to concrete worker
        public AddProject(int workerId)
        {
            this.workerId = workerId;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (nameTextBox.Text.Equals("") || !Input.IsName(nameTextBox.Text))
            {
                label1.Content = "Start with capital letter";
                nameTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (costTextBox.Text.Equals("") || !Input.IsNumber(costTextBox.Text))
            {
                
                label2.Content = "Must be integer";
                costTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if(flag == false)
                MessageBox.Show("Enter correct data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                int cost = int.Parse(costTextBox.Text);
                ProjectController projectController = new ProjectController();
                projectController.AddProject(nameTextBox.Text, cost);

                //check if we are adding new project to worker
                if (workerId != 0)
                {
                    WorkerController workerController = new WorkerController();
                    workerController.AddProject(workerId, nameTextBox.Text);
                    var owner = Owner as TableWindow;
                    if (owner != null)
                        owner.RefreshTable(new object(), new RoutedEventArgs());
                }
                MessageBox.Show("Project added successfully!", "Success");
                Close();
            }
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Avoid bug with minimizing owner window
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
