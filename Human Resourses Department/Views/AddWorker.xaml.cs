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
    /// Logic of interaction for AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        public AddWorker()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            //Form items list for each combobox
            FormDepartmentsList();
            FormPositionsList();
            FormProjectsList();
        }

        //Form items list for department combobox
        private void FormDepartmentsList()
        {
            DepartmentController departmentController = new DepartmentController();
            string name;
            foreach(var dep in departmentController.GetAllDepartmentsInfoByName())
            {
                dep.TryGetValue("name", out name);
                comboBoxDepartment.Items.Add(name);
            }
        }

        //Form items list for position combobox
        private void FormPositionsList()
        {
            PositionController positionController = new PositionController();
            string name;
            foreach(var pos in positionController.GetAllPositionsInfoByName())
            {
                pos.TryGetValue("name", out name);
                comboBoxPosition.Items.Add(name);
            }
        }

        //Form items list for project combobox
        private void FormProjectsList()
        {
            comboBoxProject.Items.Add("New project");
            ProjectController projectController = new ProjectController();
            string name;
            foreach(var proj in projectController.GetAllProjectsInfo())
            {
                proj.TryGetValue("name", out name);
                comboBoxProject.Items.Add(name);
            }
        }

        /*
         * Logic for choosing new project or existing
         * hide textboxes "name" and "cost" when existing project is choosen
         * and show them when "new project" is choosen
         */
        private void comboBoxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedItem = comboBox.SelectedItem.ToString();
            if(selectedItem.Equals("New project"))
            {
                projectNameTextBox.Text = "";
                projectCostTextBox.Text = "";
                projectNameTextBox.Visibility = Visibility.Visible;
                labelProjectName.Visibility = Visibility.Visible;
                projectCostTextBox.Visibility = Visibility.Visible;
                projectCostLabel.Visibility = Visibility.Visible;
            }
            else
            {
                projectNameTextBox.Visibility = Visibility.Collapsed;
                labelProjectName.Visibility = Visibility.Collapsed;
                projectCostTextBox.Visibility = Visibility.Collapsed;
                projectCostLabel.Visibility = Visibility.Collapsed;
                ProjectController projectController = new ProjectController();
                string name = null;
                string cost = null;
                foreach(var proj in projectController.GetAllProjectsInfo())
                {
                    string n;
                    proj.TryGetValue("name", out n);
                    if (n.Equals(selectedItem.ToString()))
                    {
                        proj.TryGetValue("name", out name);
                        proj.TryGetValue("cost", out cost);
                        break;
                    }
                }
                projectNameTextBox.Text = name;
                projectCostTextBox.Text = cost;
            }
        }

        /*
         * Logic for OK button:
         * check input
         * highlight wrong input
         * get correct input
         * create new project in database
         * show message
         * close
         */
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (nameTextBox.Text.Equals(""))
            {
                nameTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (surnameTextBox.Text.Equals(""))
            {
                surnameTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (accountNumberTextBox.Text.Equals(""))
            {
                accountNumberTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (comboBoxDepartment.SelectedItem == null)
            {
                comboBoxDepartment.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (comboBoxPosition.SelectedItem == null)
            {
                comboBoxPosition.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (experienceTextBox.Text.Equals(""))
            {
                experienceTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (comboBoxProject.SelectedItem == null)
            {
                comboBoxProject.BorderBrush = Brushes.Red;
                flag = false;
            }
            else
            {
                if (comboBoxProject.SelectedItem.ToString().Equals("New project"))
                {
                    if (projectNameTextBox.Text.Equals(""))
                    {
                        projectNameTextBox.BorderBrush = Brushes.Red;
                        flag = false;
                    }
                    if (projectCostTextBox.Text.Equals(""))
                    {
                        projectCostTextBox.BorderBrush = Brushes.Red;
                        flag = false;
                    }
                }
            }
            if (!flag)
            {
                MessageBox.Show("Fill all lines to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool projectAdded = false;
                if(comboBoxProject.SelectedItem.ToString().Equals("New project"))
                {
                    ProjectController projectController = new ProjectController();
                    int cost = int.Parse(projectCostTextBox.Text);
                    if (projectController.AddProject(projectNameTextBox.Text, cost))
                        projectAdded = true;
                }
                else
                {
                    WorkerController workerController = new WorkerController();
                    bool workerAdded = workerController.AddWorker(nameTextBox.Text, surnameTextBox.Text,
                        accountNumberTextBox.Text, comboBoxDepartment.Text, comboBoxPosition.Text,
                        int.Parse(experienceTextBox.Text), projectNameTextBox.Text);
                    if(workerAdded && projectAdded)
                    {
                        MessageBox.Show("Worker added successfully with a new project", "Success");
                        TableWindow parent = Owner as TableWindow;
                        parent.RefreshTable();
                        Close();
                    }
                    else if(workerAdded && !projectAdded)
                    {
                        MessageBox.Show("Worker added successfully", "Success");
                        TableWindow parent = Owner as TableWindow;
                        parent.RefreshTable();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong. Try again", "Error");
                    }
                }
            }
        }

        /*
         * Logic for Cancel button
         */
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
