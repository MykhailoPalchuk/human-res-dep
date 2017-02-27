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
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        WorkerController workerController;
        DepartmentController departmentController;
        PositionController positionController;
        ProjectController projectController;
        string choice;

        public TableWindow(string choice)
        {
            workerController = new WorkerController();
            departmentController = new DepartmentController();
            positionController = new PositionController();
            projectController = new ProjectController();
            this.choice = choice;
            InitializeComponent();
        }

        private void MenuItem_Click_StartPage(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
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
            }
            else
            {
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

        public void RefreshTable()
        {
            dataGrid_Loaded(new object(), new RoutedEventArgs());
        }

        private void MenuItem_Click_SortByName(object sender, RoutedEventArgs e)
        {
            dataGrid_Loaded(new object(), new RoutedEventArgs());
        }

        private void menuSortBySecondProperty_Click(object sender, RoutedEventArgs e)
        {
            if (choice.Equals("workers"))
            {
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
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menuSortByThirdProperty_Click(object sender, RoutedEventArgs e)
        {
            if (choice.Equals("workers"))
            {
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
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
