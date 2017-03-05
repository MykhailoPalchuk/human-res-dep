using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        private int workerId;

        public AddWorker()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            //Form items list for each combobox
            FormDepartmentsList();
            FormPositionsList();
            FormProjectsList();
        }

        //For changing data
        public AddWorker(int workerId)
        {
            this.workerId = workerId;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            //Form items list for each combobox
            FormDepartmentsList();
            FormPositionsList();
            FormProjectsList();

            //Set data
            var workerController = new WorkerController();
            var worker = workerController.GetWorkerInfo(workerId);
            string name, surname, account, dep, pos, exp, lastProject;
            worker.TryGetValue("name", out name);
            worker.TryGetValue("surname", out surname);
            worker.TryGetValue("accountNumber", out account);
            worker.TryGetValue("department", out dep);
            worker.TryGetValue("position", out pos);
            worker.TryGetValue("experience", out exp);
            worker.TryGetValue("project", out lastProject);

            nameTextBox.Text = name;
            surnameTextBox.Text = surname;
            accountNumberTextBox.Text = account;
            object item = "";
            foreach(var i in comboBoxDepartment.Items)
            {
                if ((i as string).Equals(dep))
                    item = i;
            }
            comboBoxDepartment.SelectedItem = item;
            foreach(var i in comboBoxPosition.Items)
            {
                if ((i as string).Equals(pos))
                    item = i;
            }
            comboBoxPosition.SelectedItem = item;
            experienceTextBox.Text = exp;
            foreach(var i in comboBoxProject.Items)
            {
                if ((i as string).Equals(lastProject))
                    item = i;
            }
            comboBoxProject.SelectedItem = item;
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
            if (nameTextBox.Text.Equals("") || !Input.IsName(nameTextBox.Text))
            {
                nameTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (surnameTextBox.Text.Equals("") || !Input.IsName(surnameTextBox.Text))
            {
                surnameTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (accountNumberTextBox.Text.Equals("") || !Input.IsAccountNumber(accountNumberTextBox.Text))
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
            if (experienceTextBox.Text.Equals("") || !Input.IsNumber(experienceTextBox.Text))
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
                MessageBox.Show("Enter correct data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                WorkerController workerController = new WorkerController();
                if (workerId != 0)
                {
                    workerController.ChangeName(workerId, nameTextBox.Text);
                    workerController.ChangeSurname(workerId, surnameTextBox.Text);
                    workerController.ChangeAccountNumber(workerId, accountNumberTextBox.Text);
                    workerController.ChangeDepartment(workerId, comboBoxDepartment.SelectedItem.ToString());
                    workerController.ChangePosition(workerId, comboBoxPosition.SelectedItem.ToString());
                    workerController.ChangeExperience(workerId, int.Parse(experienceTextBox.Text));
                    workerController.AddProject(workerId, comboBoxProject.SelectedItem.ToString());
                    MessageBox.Show("Changes saved", "Success");
                    TableWindow parent = Owner as TableWindow;
                    if (parent != null)
                        parent.RefreshTable(sender, e);
                    Close();
                }
                else
                {
                    bool workerAdded = workerController.AddWorker(nameTextBox.Text, surnameTextBox.Text,
                        accountNumberTextBox.Text, comboBoxDepartment.Text, comboBoxPosition.Text,
                        int.Parse(experienceTextBox.Text), projectNameTextBox.Text);
                    if (workerAdded && projectAdded)
                    {
                        MessageBox.Show("Worker added successfully with a new project", "Success");
                        TableWindow parent = Owner as TableWindow;
                        if (parent != null)
                            parent.RefreshTable(sender, e);
                        Close();
                    }
                    else if (workerAdded && !projectAdded)
                    {
                        MessageBox.Show("Worker added successfully", "Success");
                        TableWindow parent = Owner as TableWindow;
                        if (parent != null)
                            parent.RefreshTable(sender, e);
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
