using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;

namespace Controllers
{
    public class WorkerController
    {
        static WorkerService workerService = new WorkerService();

        public bool AddWorker(string name, string surname, string accountNumber, string departmentName,
            string positionName, int experience, string projectName)
        {
            return workerService.AddWorker(name, surname, accountNumber, departmentName,
                positionName, experience, projectName);
        }

        public bool DeleteWorker(int id)
        {
            return workerService.DeleteWorker(id);
        }

        public Dictionary<string, string> GetWorkerInfo(int id)
        {
            return workerService.GetWorkerInfo(id);
        }

        public List<Dictionary<string, string>> GetWorkersByName()
        {
            var list = workerService.GetAllWorkersInfo();
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("name", out a);
                d2.TryGetValue("name", out b);
                return a.CompareTo(b);
            });
            return list;
        }

        public List<Dictionary<string, string>> GetWorkersBySurname()
        {
            var list = workerService.GetAllWorkersInfo();
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("surname", out a);
                d2.TryGetValue("surname", out b);
                return a.CompareTo(b);
            });
            return list;
        }

        public List<Dictionary<string, string>> GetWorkersByPayment()
        {
            var list = workerService.GetAllWorkersInfo();
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("payment", out a);
                d2.TryGetValue("payment", out b);
                return a.CompareTo(b);
            });
            return list;
        }

        public bool ChangeName(int workerId, string newName)
        {
            return workerService.ChangeName(workerId, newName);
        }

        public bool ChangeSurname(int workerId, string newSurname)
        {
            return workerService.ChangeSurname(workerId, newSurname);
        }

        public bool ChangeAccountNumber(int workerId, string newAccountNumber)
        {
            return workerService.ChangeAccountNumber(workerId, newAccountNumber);
        }

        public bool ChangeDepartment(int workerId, string departmentName)
        {
            return workerService.ChangeDepartment(workerId, departmentName);
        }

        public bool ChangePosition(int workerId, string positionName)
        {
            return workerService.ChangePosition(workerId, positionName);
        }

        public bool ChangeExperience(int workerId, int newExperience)
        {
            return workerService.ChangeExperience(workerId, newExperience);
        }

        public bool AddProject(int workerId, string projectName)
        {
            return workerService.AddProject(workerId, projectName);
        }

        public List<Dictionary<string,string>> GetWorkersProjects(int id)
        {
            return workerService.GetWorkersProjects(id);
        }
    }
}
