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
using System.Windows.Shapes;
using Controllers;
using Views.Tables;

namespace Views
{
    /// <summary>
    /// Logic of interaction for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        WorkerController workerController;
        DepartmentController departmentController;
        PositionController positionController;
        ProjectController projectController;
        string choice;

        public TableWindow()
        {
            workerController = new WorkerController();
            departmentController = new DepartmentController();
            positionController = new PositionController();
            projectController = new ProjectController();
            choice = "workers";
            InitializeComponent();
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //create elements list
            if (choice.Equals("workers"))
            {
                List<WorkerTable> list = new List<WorkerTable>();
                string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
                foreach (var w in workerController.GetWorkersByName())
                {
                    w.TryGetValue("id", out id);
                    w.TryGetValue("name", out name);
                    w.TryGetValue("surname", out surname);
                    w.TryGetValue("accountNumber", out account);
                    w.TryGetValue("department", out dep);
                    w.TryGetValue("position", out pos);
                    w.TryGetValue("experience", out exp);
                    w.TryGetValue("project", out lastProject);
                    w.TryGetValue("projectsCost", out projectsCost);

                    list.Add(new WorkerTable(id, name, surname, account, dep, pos, exp, lastProject, projectsCost));
                }
                dataGrid.ItemsSource = list;

                //Create context menu
                CreateWorkerMenu(sender, e);
            }
            else if (choice.Equals("departments"))
            {
                List<DepartmentTable> list = new List<DepartmentTable>();
                string id, name, numberOfWorkers;
                foreach(var dep in departmentController.GetAllDepartmentsInfoByName())
                {
                    dep.TryGetValue("id", out id);
                    dep.TryGetValue("name", out name);
                    dep.TryGetValue("numberOfWorkers", out numberOfWorkers);

                    list.Add(new DepartmentTable(id, name, numberOfWorkers));
                }
                dataGrid.ItemsSource = list;

                //Create context menu
                CreateDepartmentMenu(sender, e);

                //change menu
                menuSortBySecondProperty.Header = "By number of workers";
                menuSortByThirdProperty.Visibility = Visibility.Collapsed;
            }
            else if (choice.Equals("positions"))
            {
                List<PositionTable> list = new List<PositionTable>();
                string id, name, hours, payment, totalPayment;
                foreach(var pos in positionController.GetAllPositionsInfoByName())
                {
                    pos.TryGetValue("id", out id);
                    pos.TryGetValue("name", out name);
                    pos.TryGetValue("hours", out hours);
                    pos.TryGetValue("payment", out payment);
                    pos.TryGetValue("totalPayment", out totalPayment);

                    list.Add(new PositionTable(id, name, hours, payment, totalPayment));
                }
                dataGrid.ItemsSource = list;

                //Create context menu
                CreatePositionMenu(sender, e);

                //change menu
                menuSortBySecondProperty.Header = "By payment";
                menuSortByThirdProperty.Visibility = Visibility.Collapsed;
            }
            else
            {
                //something impossible but should be checked
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_AddProject(object sender, RoutedEventArgs e)
        {
            AddProject project = new AddProject();
            project.Owner = this;
            project.Show();
        }

        private void MenuItem_Click_AddPosition(object sender, RoutedEventArgs e)
        {
            AddPosition position = new AddPosition();
            position.Owner = this;
            position.Show();
        }

        private void MenuItem_Click_AddDepartment(object sender, RoutedEventArgs e)
        {
            AddDepartment department = new AddDepartment();
            department.Owner = this;
            department.Show();
        }

        private void MenuItem_Click_AddWorker(object sender, RoutedEventArgs e)
        {
            AddWorker worker = new AddWorker();
            worker.Owner = this;
            worker.Show();
        }

        //Reload table after adding new elements to database
        public void RefreshTable(object sender, RoutedEventArgs e)
        {
            dataGrid_Loaded(sender, e);
        }

        //Default sorting, so we just reload dataGrid
        private void MenuItem_Click_SortByName(object sender, RoutedEventArgs e)
        {
            dataGrid_Loaded(new object(), new RoutedEventArgs());
        }

        private void menuSortBySecondProperty_Click(object sender, RoutedEventArgs e)
        {
            if (choice.Equals("workers"))
            {
                //Sort by surname
                List<WorkerTable> list = new List<WorkerTable>();
                string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
                foreach (var w in workerController.GetWorkersBySurname())
                {
                    w.TryGetValue("id", out id);
                    w.TryGetValue("name", out name);
                    w.TryGetValue("surname", out surname);
                    w.TryGetValue("accountNumber", out account);
                    w.TryGetValue("department", out dep);
                    w.TryGetValue("position", out pos);
                    w.TryGetValue("experience", out exp);
                    w.TryGetValue("project", out lastProject);
                    w.TryGetValue("projectsCost", out projectsCost);

                    list.Add(new WorkerTable(id, name, surname, account, dep, pos, exp, lastProject, projectsCost));
                }
                dataGrid.ItemsSource = list;
            }
            else if (choice.Equals("departments"))
            {
                //Sort by number of workers
                List<DepartmentTable> list = new List<DepartmentTable>();
                string id, name, numberOfWorkers;
                foreach (var dep in departmentController.GetAllDepartmentsInfoByNumberOfWorkers())
                {
                    dep.TryGetValue("id", out id);
                    dep.TryGetValue("name", out name);
                    dep.TryGetValue("numberOfWorkers", out numberOfWorkers);

                    list.Add(new DepartmentTable(id, name, numberOfWorkers));
                }
                dataGrid.ItemsSource = list;
            }
            else if (choice.Equals("positions"))
            {
                //Sort by payment
                List<PositionTable> list = new List<PositionTable>();
                string id, name, hours, payment, totalPayment;
                foreach (var pos in positionController.GetAllPositionsInfoByPayment())
                {
                    pos.TryGetValue("id", out id);
                    pos.TryGetValue("name", out name);
                    pos.TryGetValue("hours", out hours);
                    pos.TryGetValue("payment", out payment);
                    pos.TryGetValue("totalPayment", out totalPayment);

                    list.Add(new PositionTable(id, name, hours, payment, totalPayment));
                }
                dataGrid.ItemsSource = list;
            }
            else
            {
                //something impossible but should be checked
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menuSortByThirdProperty_Click(object sender, RoutedEventArgs e)
        {
            if (choice.Equals("workers"))
            {
                //Sort by payment
                List<WorkerTable> list = new List<WorkerTable>();
                string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
                foreach (var w in workerController.GetWorkersByPayment())
                {
                    w.TryGetValue("id", out id);
                    w.TryGetValue("name", out name);
                    w.TryGetValue("surname", out surname);
                    w.TryGetValue("accountNumber", out account);
                    w.TryGetValue("department", out dep);
                    w.TryGetValue("position", out pos);
                    w.TryGetValue("experience", out exp);
                    w.TryGetValue("project", out lastProject);
                    w.TryGetValue("projectsCost", out projectsCost);

                    list.Add(new WorkerTable(id, name, surname, account, dep, pos, exp, lastProject, projectsCost));
                }
                dataGrid.ItemsSource = list;
            }
            else
            {
                //something impossible but should be checked
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Build context menu for workers
        private void CreateWorkerMenu(object sender, RoutedEventArgs e)
        {
            //avoid NullReferenceException
            DataGrid dataG;
            if (dataGrid.ContextMenu == null)
                dataG = sender as DataGrid;
            else
                dataG = dataGrid;
            dataG.ContextMenu.Items.Clear();
            var item = new MenuItem();
            item.Header = "Change data";
            item.Click += WorkerChangeData;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Delete";
            item.Click += WorkerDelete;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Add project";
            item.Click += WorkerAddProject;
            dataG.ContextMenu.Items.Add(item);
        }

        //Build context menu for departments
        private void CreateDepartmentMenu(object sender, RoutedEventArgs e)
        {
            //Avoid NullReferenceException
            DataGrid dataG;
            if (dataGrid.ContextMenu == null)
                dataG = sender as DataGrid;
            else
                dataG = dataGrid;
            dataG.ContextMenu.Items.Clear();
            var item = new MenuItem();
            item.Header = "Change data";
            item.Click += DepartmentChangeData;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Delete";
            item.Click += DepartmentDelete;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Show workers by position";
            item.Click += DepartmentShowWorkersByPosition;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Show workers by projects";
            item.Click += DepartmentShowWorkersByProjects;
            dataG.ContextMenu.Items.Add(item);
        }

        //Build context menu for positions
        private void CreatePositionMenu(object sender, RoutedEventArgs e)
        {
            //Avoid NullReferenceException
            DataGrid dataG;
            if (dataGrid.ContextMenu == null)
                dataG = sender as DataGrid;
            else
                dataG = dataGrid;
            dataG.ContextMenu.Items.Clear();
            var item = new MenuItem();
            item.Header = "Change data";
            item.Click += PositionChangeData;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Delete";
            item.Click += PositionDelete;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Show top workers";
            item.Click += PositionShowWorkers;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Show top positions";
            item.Click += PositionShowTop;
            dataG.ContextMenu.Items.Add(item);
        }

        private void WorkerChangeData(object sender, RoutedEventArgs e)
        {
            
        }

        private void WorkerDelete(object sender, RoutedEventArgs e)
        {

        }

        private void WorkerAddProject(object sender, RoutedEventArgs e)
        {

        }

        private void DepartmentChangeData(object sender, RoutedEventArgs e)
        {

        }

        private void DepartmentDelete(object sender, RoutedEventArgs e)
        {

        }

        private void DepartmentShowWorkersByPosition(object sender, RoutedEventArgs e)
        {
            
        }

        private void DepartmentShowWorkersByProjects(object sender, RoutedEventArgs e)
        {

        }

        private void PositionChangeData(object sender, RoutedEventArgs e)
        {

        }

        private void PositionDelete(object sender, RoutedEventArgs e)
        {

        }

        private void PositionShowWorkers(object sender, RoutedEventArgs e)
        {

        }

        private void PositionShowTop(object sender, RoutedEventArgs e)
        {

        }

        private void ShowWorkers(object sender, RoutedEventArgs e)
        {
            choice = "workers";
            RefreshTable(sender, e);
        }

        private void ShowDepartments(object sender, RoutedEventArgs e)
        {
            choice = "departments";
            RefreshTable(new object(), new RoutedEventArgs());
        }

        private void ShowPositions(object sender, RoutedEventArgs e)
        {
            choice = "positions";
            RefreshTable(sender, e);
        }
    }
}
