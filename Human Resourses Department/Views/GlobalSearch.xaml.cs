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
    /// Logic of interaction for GlobalSearch.xaml
    /// </summary>
    public partial class GlobalSearch : Window
    {
        private SearchEngine searchEngine;
        private string key;

        public GlobalSearch(string key)
        {
            this.key = key;
            searchEngine = new SearchEngine();
            InitializeComponent();
            Title = "Global search results for \"" + key + "\"";
            ResizeMode = ResizeMode.CanMinimize;
        }

        private void FormWorkersResults()
        {
            var result = searchEngine.SearchForWorkers(key);
            var list = new List<WorkerTable>();
            string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
            foreach (var w in result)
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
            workersDataGrid.ItemsSource = list;
        }

        private void FormDepartmentsResults()
        {
            var result = searchEngine.SearchForDepartments(key);
            var list = new List<DepartmentTable>();
            string id, name, numberOfWorkers;
            foreach (var dep in result)
            {
                dep.TryGetValue("id", out id);
                dep.TryGetValue("name", out name);
                dep.TryGetValue("numberOfWorkers", out numberOfWorkers);

                list.Add(new DepartmentTable(id, name, numberOfWorkers));
            }
            departmentsDataGrid.ItemsSource = list;
        }

        private void FormPositionsResults()
        {
            var result = searchEngine.SearchForPositions(key);
            var list = new List<PositionTable>();
            string id, name, hours, payment, totalPayment;
            foreach (var pos in result)
            {
                pos.TryGetValue("id", out id);
                pos.TryGetValue("name", out name);
                pos.TryGetValue("hours", out hours);
                pos.TryGetValue("payment", out payment);
                pos.TryGetValue("totalPayment", out totalPayment);

                list.Add(new PositionTable(id, name, hours, payment, totalPayment));
            }
            positionsDataGrid.ItemsSource = list;
        }

        private void FormProjectsResults()
        {
            var result = searchEngine.SearchForProjects(key);
            var list = new List<ProjectTable>();
            string id, name, cost, numberOfWorkers;
            foreach (var p in result)
            {
                p.TryGetValue("id", out id);
                p.TryGetValue("name", out name);
                p.TryGetValue("cost", out cost);
                p.TryGetValue("numberOfWorkers", out numberOfWorkers);

                list.Add(new ProjectTable(id, name, cost, numberOfWorkers));
            }
            projectsDataGrid.ItemsSource = list;
        }

        private void FormWorkersMenu()
        {
            var dataG = workersDataGrid;
            dataG.ContextMenu = new ContextMenu();
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

        private void WorkerChangeData(object sender, RoutedEventArgs e)
        {
            var worker = workersDataGrid.SelectedItem as WorkerTable;
            if (worker != null)
            {
                var workerWindow = new AddWorker(int.Parse(worker.Id));
                workerWindow.Owner = this;
                workerWindow.Show();
            }
        }

        private void WorkerDelete(object sender, RoutedEventArgs e)
        {
            var worker = workersDataGrid.SelectedItem as WorkerTable;
            if (worker != null)
            {
                var workerController = new WorkerController();
                var res = MessageBox.Show("Worker " + worker.Name + " " + worker.Surname + " will be deleted", "Warning",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    int id = int.Parse(worker.Id);
                    if (workerController.DeleteWorker(id))
                    {
                        MessageBox.Show("Worker deleted successfully");
                        workersDataGrid.Items.Remove(worker);
                    }
                    else
                        MessageBox.Show("Error", "Error");
                }
            }
        }

        private void WorkerAddProject(object sender, RoutedEventArgs e)
        {
            WorkerTable worker = workersDataGrid.SelectedItem as WorkerTable;
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

        private void FormDepartmentsMenu()
        {
            var dataG = departmentsDataGrid;
            dataG.ContextMenu = new ContextMenu();
            var item = new MenuItem();
            item.Header = "Change data";
            item.Click += DepartmentChangeData;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Delete";
            item.Click += DepartmentDelete;
            dataG.ContextMenu.Items.Add(item);
        }

        private void DepartmentChangeData(object sender, RoutedEventArgs e)
        {
            var department = departmentsDataGrid.SelectedItem as WorkerTable;
            if (department != null)
            {
                var workerWindow = new AddDepartment(true, int.Parse(department.Id));
                workerWindow.Owner = this;
                workerWindow.Show();
            }
        }

        private void DepartmentDelete(object sender, RoutedEventArgs e)
        {
            var dep = departmentsDataGrid.SelectedItem as DepartmentTable;
            if (dep != null)
            {
                var departmentController = new DepartmentController();
                var res = MessageBox.Show("Department " + dep.Name + " will be deleted", "Warning",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    int id = int.Parse(dep.Id);
                    if (departmentController.DeleteDepartment(id))
                    {
                        MessageBox.Show("Department deleted successfully");
                        departmentsDataGrid.Items.Remove(dep);
                    }
                    else
                        MessageBox.Show("Error", "Error");
                }
            }
        }

        private void FormPositionsMenu()
        {
            var dataG = positionsDataGrid;
            dataG.ContextMenu = new ContextMenu();
            var item = new MenuItem();
            item.Header = "Change data";
            item.Click += PositionChangeData;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
            item.Header = "Delete";
            item.Click += PositionDelete;
            dataG.ContextMenu.Items.Add(item);
            item = new MenuItem();
        }

        private void PositionChangeData(object sender, RoutedEventArgs e)
        {
            var position = positionsDataGrid.SelectedItem as PositionTable;
            if (position != null)
            {
                var positionWindow = new AddPosition(int.Parse(position.Id));
                positionWindow.Owner = this;
                positionWindow.Show();
            }
        }

        private void PositionDelete(object sender, RoutedEventArgs e)
        {
            var pos = positionsDataGrid.SelectedItem as PositionTable;
            if (pos != null)
            {
                var positionController = new PositionController();
                var res = MessageBox.Show("Position " + pos.Name + " will be deleted", "Warning",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    int id = int.Parse(pos.Id);
                    if (positionController.DeletePosition(id))
                    {
                        MessageBox.Show("Position deleted successfully");
                        positionsDataGrid.Items.Remove(pos);
                    }
                    else
                        MessageBox.Show("Error", "Error");
                }
            }
        }

        private void FormProjectsMenu()
        {
            var dataG = projectsDataGrid;
            dataG.ContextMenu = new ContextMenu();
            var item = new MenuItem();
            item.Header = "Delete";
            item.Click += ProjectDelete;
            dataG.ContextMenu.Items.Add(item);
        }

        private void ProjectDelete(object sender, RoutedEventArgs e)
        {
            var project = projectsDataGrid.SelectedItem as ProjectTable;
            if (project != null)
            {
                var projectController = new ProjectController();
                if (projectController.DeleteProject(int.Parse(project.Id)))
                {
                    MessageBox.Show("Project deleted successfully");
                    projectsDataGrid.Items.Remove(project);
                }
                else
                    MessageBox.Show("Error", "Error");
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void workersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            FormWorkersResults();
            FormWorkersMenu();
        }

        private void positionsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            FormPositionsResults();
            FormPositionsMenu();
        }

        private void departmentsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            FormDepartmentsResults();
            FormDepartmentsMenu();
        }

        private void projectsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            FormProjectsResults();
            FormProjectsMenu();
        }
    }
}
