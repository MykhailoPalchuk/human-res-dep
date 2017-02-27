using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;

namespace Controllers
{
    public class SearchEngine
    {
        static DepartmentService departmentService = new DepartmentService();
        static PositionService positionService = new PositionService();
        static ProjectService projectService = new ProjectService();
        static WorkerService workerService = new WorkerService();

        public List<Dictionary<string, string>> SearchForWorkers(string key)
        {
            var workers = new List<Dictionary<string, string>>();
            foreach (var w in workerService.GetAllWorkersInfo())
            {
                string name, surname, accountNumber;
                w.TryGetValue("name", out name);
                w.TryGetValue("surname", out surname);
                w.TryGetValue("accountNumber", out accountNumber);
                if (name.Contains(key) || surname.Contains(key) || accountNumber.Contains(key))
                    workers.Add(w);
            }
            return workers;
        }

        public List<Dictionary<string, string>> SearchForProjects(string key)
        {
            var projects = new List<Dictionary<string, string>>();
            foreach(var p in projectService.GetAllProjectsInfo())
            {
                string name;
                p.TryGetValue("name", out name);
                if (name.Contains(key))
                    projects.Add(p);
            }
            return projects;
        }

        public List<Dictionary<string, string>> SearchForDepartments(string key)
        {
            var departments = new List<Dictionary<string, string>>();
            foreach(var d in departmentService.GetAllDepartmentsInfo())
            {
                string name;
                d.TryGetValue("name", out name);
                if (name.Contains(key))
                    departments.Add(d);
            }
            return departments;
        }

        public List<Dictionary<string, string>> SearchForPositions(string key)
        {
            var positions = new List<Dictionary<string, string>>();
            foreach (var p in positionService.GetAllPositionsInfo())
            {
                string name;
                p.TryGetValue("name", out name);
                if (name.Contains(key))
                    positions.Add(p);
            }
            return positions;
        }

        public Dictionary<string, string> SearchForWorker(string name, string surname)
        {
            Dictionary<string, string> worker = null;
            foreach(var w in workerService.GetAllWorkersInfo())
            {
                string n, s;
                w.TryGetValue("name", out n);
                w.TryGetValue("surname", out s);
                if (n.Equals(name) && s.Equals(surname))
                    worker = w;
            }
            return worker;
        }
    }
}
