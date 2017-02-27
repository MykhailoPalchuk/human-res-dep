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

namespace Views
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        WorkerController workerController;
        public Workers()
        {
            workerController = new WorkerController();
            InitializeComponent();
            WindowState = WindowState.Maximized;
            //FormListByName();
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MenuItem_Click_StartPage(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void FormListByName()
        {
            table.Items.Clear();
            table.Items.Add(new WorkerTable());
            foreach(var w in workerController.GetWorkersByName())
            {
                string id, name, surname, account, dep, pos, exp;
                w.TryGetValue("id", out id);
                w.TryGetValue("name", out name);
                w.TryGetValue("surname", out surname);
                w.TryGetValue("accountNumber", out account);
                w.TryGetValue("department", out dep);
                w.TryGetValue("position", out pos);
                w.TryGetValue("experience", out exp);

                table.Items.Add(new WorkerTable(id, name, surname, account, dep, pos, exp));
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

        }
    }

    class WorkerTable
    {
        public WorkerTable()
        {
            Id = "Id";
            Name = "Name";
            Surname = "Surname";
            AccountNumber = "Account number";
            DepartmentName = "Department";
            PositionName = "Position";
            Experience = "Experience";
        }

        public WorkerTable(string id, string name, string surname, string accountNumber, 
            string departmentName, string positionName, string experience)
        {
            Id = id;
            Name = name;
            Surname = surname;
            AccountNumber = accountNumber;
            DepartmentName = departmentName;
            PositionName = positionName;
            Experience = experience;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AccountNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string Experience { get; set; }
    }
}
