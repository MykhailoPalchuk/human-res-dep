using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;

namespace Controllers
{
    public class DepartmentController
    {
        static DepartmentService departmentService = new DepartmentService();

        public bool AddDepartment(string name)
        {
            return departmentService.AddDepartment(name);
        }

        public bool DeleteDepartment(int id)
        {
            return departmentService.DeleteDepartment(id);
        }

        public bool ChangeName(int departmentId, string newName)
        {
            return departmentService.ChangeName(departmentId, newName);
        }

        public Dictionary<string, string> GetDepartmentInfo(int departmentId)
        {
            return departmentService.GetDepartmentInfo(departmentId);
        }

        public List<Dictionary<string, string>> GetAllDepartmentsInfoByName()
        {
            var list = departmentService.GetAllDepartmentsInfo();
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("name", out a);
                d2.TryGetValue("name", out b);
                return a.CompareTo(b);
            });
            return list;
        }

        public List<Dictionary<string,string>> GetAllDepartmentsInfoByNumberOfWorkers()
        {
            var list = departmentService.GetAllDepartmentsInfo();
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("numberOfWorkers", out a);
                d2.TryGetValue("numberOfWorkers", out b);
                return a.CompareTo(b);
            });
            return list;
        }

        public List<Dictionary<string,string>> GetWorkersByPosition(int departmentId)
        {
            var list = departmentService.GetWorkersInfo(departmentId);
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("position", out a);
                d2.TryGetValue("position", out b);
                return a.CompareTo(b);
            });
            return list;
        }

        public List<Dictionary<string, string>> GetWorkersByProjectsCost(int departmentId)
        {
            var list = departmentService.GetWorkersInfo(departmentId);
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("projectsCost", out a);
                d2.TryGetValue("projectsCost", out b);
                return a.CompareTo(b);
            });
            return list;
        }
    }
}
