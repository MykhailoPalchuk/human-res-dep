using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
                Title = "Workers";
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
                Title = "Departments";
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
                Title = "Positions";
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
            AddDepartment department = new AddDepartment(false, 0);
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
            dataG.ContextMenu.Visibility = Visibility.Visible;
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
            dataG.ContextMenu.Visibility = Visibility.Visible;
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
            dataG.ContextMenu.Visibility = Visibility.Visible;
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
            var worker = dataGrid.SelectedItem as WorkerTable;
            if (worker != null)
            {
                var workerWindow = new AddWorker(int.Parse(worker.Id));
                workerWindow.Owner = this;
                workerWindow.Show();
            }
        }

        private void WorkerDelete(object sender, RoutedEventArgs e)
        {
            var worker = dataGrid.SelectedItem as WorkerTable;
            if (worker != null)
            {
                var res = MessageBox.Show("Worker " + worker.Name + " " + worker.Surname + " will be deleted", "Warning",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    int id = int.Parse(worker.Id);
                    if (workerController.DeleteWorker(id))
                        MessageBox.Show("Worker deleted successfully");
                    else
                        MessageBox.Show("Error", "Error");
                    RefreshTable(sender, e);
                }
            }
        }
        
        private void WorkerAddProject(object sender, RoutedEventArgs e)
        {
            WorkerTable worker = dataGrid.SelectedItem as WorkerTable;
            if (worker != null)
            {
                var res = MessageBox.Show("Create new project?", "Add project",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    var addProjectWindow = new AddProject(int.Parse(worker.Id));
                    addProjectWindow.Owner = this;
                    addProjectWindow.Show();
                }
                else if (res == MessageBoxResult.No)
                {
                    var chooseProject = new ChooseProject(worker);
                    chooseProject.Owner = this;
                    chooseProject.Show();
                }
            }
        }

        private void DepartmentChangeData(object sender, RoutedEventArgs e)
        {
            var dep = dataGrid.SelectedItem as DepartmentTable;
            if (dep != null)
            {
                int id = int.Parse(dep.Id);
                AddDepartment department = new AddDepartment(true, id);
                department.Owner = this;
                department.Show();
            }
        }

        private void DepartmentDelete(object sender, RoutedEventArgs e)
        {
            var dep = dataGrid.SelectedItem as DepartmentTable;
            if (dep != null)
            {
                var res = MessageBox.Show("Department " + dep.Name + " will be deleted", "Warning",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    int id = int.Parse(dep.Id);
                    if (departmentController.DeleteDepartment(id))
                        MessageBox.Show("Department deleted successfully");
                    else
                        MessageBox.Show("Error", "Error");
                    RefreshTable(sender, e);
                }
            }
        }

        private void DepartmentShowWorkersByPosition(object sender, RoutedEventArgs e)
        {
            var department = dataGrid.SelectedItem as DepartmentTable;
            if (department != null)
            {
                var workers = departmentController.GetWorkersByPosition(int.Parse(department.Id));
                if (workers == null || workers.Count == 0)
                    MessageBox.Show("There are no workers in this department", "No workers",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    var list = new List<WorkerTable>();
                    foreach (var w in workers)
                    {
                        string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
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
                    CreateWorkerMenu(new object(), new RoutedEventArgs());
                    Title = "Workers";
                }
            }
        }

        private void DepartmentShowWorkersByProjects(object sender, RoutedEventArgs e)
        {
            var department = dataGrid.SelectedItem as DepartmentTable;
            if (department != null)
            {
                var workers = departmentController.GetWorkersByProjectsCost(int.Parse(department.Id));
                if (workers == null || workers.Count == 0)
                    MessageBox.Show("There are no workers in this department", "No workers",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    var list = new List<WorkerTable>();
                    foreach (var w in workers)
                    {
                        string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
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
                    Title = "Workers";
                }
            }
        }
        
        private void PositionChangeData(object sender, RoutedEventArgs e)
        {
            var position = dataGrid.SelectedItem as PositionTable;
            if (position != null)
            {
                var positionWindow = new AddPosition(int.Parse(position.Id));
                positionWindow.Owner = this;
                positionWindow.Show();
            }
        }

        private void PositionDelete(object sender, RoutedEventArgs e)
        {
            var pos = dataGrid.SelectedItem as PositionTable;
            if (pos != null)
            {
                var res = MessageBox.Show("Position " + pos.Name + " will be deleted", "Warning",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    int id = int.Parse(pos.Id);
                    if (positionController.DeletePosition(id))
                        MessageBox.Show("Position deleted successfully");
                    else
                        MessageBox.Show("Error", "Error");
                    RefreshTable(sender, e);
                }
            }
        }

        private void PositionShowWorkers(object sender, RoutedEventArgs e)
        {
            var position = dataGrid.SelectedItem as PositionTable;
            if (position != null)
            {
                var w = positionController.GetTopWorker(int.Parse(position.Id));
                if (w == null)
                    MessageBox.Show("There are no workers on this position", "No workers",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
                    w.TryGetValue("id", out id);
                    w.TryGetValue("name", out name);
                    w.TryGetValue("surname", out surname);
                    w.TryGetValue("accountNumber", out account);
                    w.TryGetValue("department", out dep);
                    w.TryGetValue("position", out pos);
                    w.TryGetValue("experience", out exp);
                    w.TryGetValue("project", out lastProject);
                    w.TryGetValue("projectsCost", out projectsCost);

                    var list = new List<WorkerTable>();
                    list.Add(new WorkerTable(id, name, surname, account, dep, pos, exp, lastProject, projectsCost));
                    dataGrid.ItemsSource = list;
                    CreateWorkerMenu(new object(), new RoutedEventArgs());
                    Title = "Workers";
                }
            }
        }

        private void PositionShowTop(object sender, RoutedEventArgs e)
        {
            var positions = positionController.GetTopFivePositions();
            var list = new List<PositionTable>();
            if(positions == null)
                MessageBox.Show("There are no positions", "No positions",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                string id, name, hours, payment, totalPayment;
                foreach (var pos in positions)
                {
                    pos.TryGetValue("id", out id);
                    pos.TryGetValue("name", out name);
                    pos.TryGetValue("hours", out hours);
                    pos.TryGetValue("payment", out payment);
                    pos.TryGetValue("totalPayment", out totalPayment);

                    list.Add(new PositionTable(id, name, hours, payment, totalPayment));
                }
                dataGrid.ItemsSource = list;
                CreatePositionMenu(new object(), new RoutedEventArgs());
                Title = "Positions";
            }
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

        public void ShowSearchResults(List<object> results)
        {
            if (results.Count == 0)
            {
                MessageBox.Show("No results", "Results", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (results[0] as WorkerTable != null)
            {
                var source = new List<WorkerTable>();
                foreach (var item in results)
                {
                    source.Add(item as WorkerTable);
                }
                dataGrid.ItemsSource = source;
                CreateWorkerMenu(new object(), new RoutedEventArgs());
                Title = "Workers";
            }
            else if (results[0] as DepartmentTable != null)
            {
                var source = new List<DepartmentTable>();
                foreach (var item in results)
                {
                    source.Add(item as DepartmentTable);
                }
                dataGrid.ItemsSource = source;
                CreateDepartmentMenu(new object(), new RoutedEventArgs());
                Title = "Departments";
            }
            else if (results[0] as PositionTable != null)
            {
                var source = new List<PositionTable>();
                foreach (var item in results)
                {
                    source.Add(item as PositionTable);
                }
                dataGrid.ItemsSource = source;
                CreatePositionMenu(new object(), new RoutedEventArgs());
                Title = "Positions";
            }
            else if (results[0] as ProjectTable != null)
            {
                var source = new List<ProjectTable>();
                foreach (var item in results)
                {
                    source.Add(item as ProjectTable);
                }
                dataGrid.ItemsSource = source;
                dataGrid.ContextMenu.Visibility = Visibility.Hidden;
                Title = "Projects";
            }
            else
                MessageBox.Show("Something went wrong. Looks like you crashed everything!", "Unknown eror",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SearchWorkers(object sender, RoutedEventArgs e)
        {
            KeyWindow keyWindow = new KeyWindow("workers");
            keyWindow.Owner = this;
            keyWindow.Show();
        }

        private void SearchDepartments(object sender, RoutedEventArgs e)
        {
            KeyWindow keyWindow = new KeyWindow("departments");
            keyWindow.Owner = this;
            keyWindow.Show();
        }

        private void SearchPositions(object sender, RoutedEventArgs e)
        {
            KeyWindow keyWindow = new KeyWindow("positions");
            keyWindow.Owner = this;
            keyWindow.Show();
        }

        private void SearchProjects(object sender, RoutedEventArgs e)
        {
            KeyWindow keyWindow = new KeyWindow("projects");
            keyWindow.Owner = this;
            keyWindow.Show();
        }

        private void SearchGlobal(object sender, RoutedEventArgs e)
        {
            KeyWindow keyWindow = new KeyWindow("global");
            keyWindow.Owner = this;
            keyWindow.Show();
        }
    }
}
