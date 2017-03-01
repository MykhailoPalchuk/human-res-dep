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
using Views.Tables;
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for ChooseProject.xaml
    /// </summary>
    public partial class ChooseProject : Window
    {
        WorkerTable worker;

        public ChooseProject(WorkerTable worker)
        {
            this.worker = worker;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            FormProjectsList();
        }

        /*
         * Logic for OK button:
         * check input
         * highlight wrong input
         * get correct input
         * add project to worker
         * show message
         * close
         */
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox == null || comboBox.Text.Equals(""))
            {
                comboBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Choose any project to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                WorkerController workerController = new WorkerController();
                if(workerController.AddProject(int.Parse(worker.Id), comboBox.Text))
                    MessageBox.Show("Project added successfully!", "Success");
                else
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                var owner = Owner as TableWindow;
                owner.RefreshTable(new object(), new RoutedEventArgs());
            }
        }

        /*
         * Logic for Cancel button
         */
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Form items list for project combobox
        private void FormProjectsList()
        {
            ProjectController projectController = new ProjectController();
            string name;
            foreach (var proj in projectController.GetAllProjectsInfo())
            {
                proj.TryGetValue("name", out name);
                comboBox.Items.Add(name);
            }
        }

        /*
         * Avoid bug with minimizing owner window
         */
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Owner = null;
        }

        /*
         * Set enter button as OK button click
         */
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OK_Click(new object(), new RoutedEventArgs());
        }
    }
}
