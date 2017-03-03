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

        public GlobalSearch(string key)
        {
            Title = "Global search results for \"" + key + "\"";
            searchEngine = new SearchEngine();
            InitializeComponent();
            ResizeMode = ResizeMode.CanMinimize;
            FormWorkersResults(key);
            FormDepartmentsResults(key);
            FormPositionsResults(key);
            FormProjectsResults(key);
        }

        private void FormWorkersResults(string key)
        {
            var result = searchEngine.SearchForWorkers(key);
            if (result.Count == 0)
                workersDataGrid.Items.Add("No results");
            else
            {
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
        }

        private void FormDepartmentsResults(string key)
        {
            var result = searchEngine.SearchForDepartments(key);
            if (result.Count == 0)
                departmentsDataGrid.Items.Add("No results");
            else
            {
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
        }

        private void FormPositionsResults(string key)
        {
            var result = searchEngine.SearchForPositions(key);
            if (result.Count == 0)
                positionsDataGrid.Items.Add("No results");
            else
            {
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
        }

        private void FormProjectsResults(string key)
        {
            var result = searchEngine.SearchForProjects(key);
            if (result.Count == 0)
                projectsDataGrid.Items.Add("No results");
            else
            {
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
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
