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
using Views.Tables;

namespace Views
{
    /// <summary>
    /// Logic of interaction for KeyWindow.xaml
    /// </summary>
    public partial class KeyWindow : Window
    {
        private SearchEngine searchEngine;
        private string choice;

        public KeyWindow(string choice)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            textBox.Focus();
            this.choice = choice;
            searchEngine = new SearchEngine();
        }

        //Logic for Cancel button
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

        //Set enter button as OK button click
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OK_Click(new object(), new RoutedEventArgs());
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Equals(""))
            {
                MessageBox.Show("Enter key word", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.BorderBrush = Brushes.Red;
            }
            else
            {
                var owner = Owner as TableWindow;
                switch (choice)
                {
                    case "workers":
                        owner.ShowSearchResults(GetWorkersResults());
                        Close();
                        break;
                    case "departments":
                        owner.ShowSearchResults(GetDepartmentsResults());
                        Close();
                        break;
                    case "positions":
                        owner.ShowSearchResults(GetPositionsResults());
                        Close();
                        break;
                    case "projects":
                        owner.ShowSearchResults(GetProjectResults());
                        Close();
                        break;
                    case "global":
                        GlobalSearch globalSearchWindow = new GlobalSearch(textBox.Text);
                        globalSearchWindow.Show();
                        Close();
                        break;
                }
            }
        }

        private List<object> GetWorkersResults()
        {
            var list = searchEngine.SearchForWorkers(textBox.Text);
            var res = new List<object>();
            string id, name, surname, account, dep, pos, exp, lastProject, projectsCost;
            foreach (var w in list)
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

                res.Add(new WorkerTable(id, name, surname, account, dep, pos, exp, lastProject, projectsCost));
            }
            return res;
        }

        private List<object> GetDepartmentsResults()
        {
            var list = searchEngine.SearchForDepartments(textBox.Text);
            var res = new List<object>();
            string id, name, numberOfWorkers;
            foreach (var dep in list)
            {
                dep.TryGetValue("id", out id);
                dep.TryGetValue("name", out name);
                dep.TryGetValue("numberOfWorkers", out numberOfWorkers);

                res.Add(new DepartmentTable(id, name, numberOfWorkers));
            }
            return res;
        }

        private List<object> GetPositionsResults()
        {
            var list = searchEngine.SearchForPositions(textBox.Text);
            var res = new List<object>();
            string id, name, hours, payment, totalPayment;
            foreach (var pos in list)
            {
                pos.TryGetValue("id", out id);
                pos.TryGetValue("name", out name);
                pos.TryGetValue("hours", out hours);
                pos.TryGetValue("payment", out payment);
                pos.TryGetValue("totalPayment", out totalPayment);

                res.Add(new PositionTable(id, name, hours, payment, totalPayment));
            }
            return res;
        }

        private List<object> GetProjectResults()
        {
            var list = searchEngine.SearchForProjects(textBox.Text);
            var res = new List<object>();
            string id, name, cost, numberOfWorkers;
            foreach(var p in list)
            {
                p.TryGetValue("id", out id);
                p.TryGetValue("name", out name);
                p.TryGetValue("cost", out cost);
                p.TryGetValue("numberOfWorkers", out numberOfWorkers);

                res.Add(new ProjectTable(id, name, cost, numberOfWorkers));
            }
            return res;
        }
    }
}
