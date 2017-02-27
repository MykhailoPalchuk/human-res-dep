using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Models.Services
{
    public class DepartmentService
    {
        static WorkerService workerService = new WorkerService();

        public bool AddDepartment(string name)
        {
            if (FindDepartment(name) == null)
            {
                using (var db = new DataContext())
                {
                    db.Departments.Add(new Department { Name = name });
                    db.SaveChanges();
                    return true;
                }
            }
            else
                return false;
        }

        public bool DeleteDepartment(int id)
        {
            using (var db = new DataContext())
            {
                if (db.Departments.Find(id) != null)
                {
                    db.Departments.Remove(db.Departments.Find(id));
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public Department FindDepartment(string name)
        {
            using(var db = new DataContext())
            {
                foreach(var dep in db.Departments.ToList())
                {
                    if (dep.Name.Equals(name))
                        return dep;
                }
                return null;
            }
        }

        public Department FindDepartment(int id)
        {
            using (var db = new DataContext())
            {
                return db.Departments.Find(id);
            }
        }

        public Dictionary<string, string> GetDepartmentInfo(int id)
        {
            using (var db = new DataContext())
            {
                var list = db.Departments.Include(d => d.Workers);
                var query = from dep in list
                            where dep.Id == id
                            select dep;
                var depart = query.FirstOrDefault();
                if (depart != null)
                {
                    Dictionary<string, string> department = new Dictionary<string, string>();

                    department.Add("id", id.ToString());
                    department.Add("name", depart.Name);
                    department.Add("numberOfWorkers", depart.Workers.Count.ToString());
                    return department;
                }
                else
                    return null;
            }
        }

        public List<Dictionary<string,string>> GetAllDepartmentsInfo()
        {
            using (var db = new DataContext())
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                foreach(var dep in db.Departments.ToList())
                {
                    list.Add(GetDepartmentInfo(dep.Id));
                }
                return list;
            }
        }

        public List<Dictionary<string,string>> GetWorkersInfo(int departmentId)
        {
            using (var db = new DataContext())
            {
                var query = from dep in db.Departments.Include(d => d.Workers)
                            where dep.Id == departmentId
                            select dep;
                var department = query.FirstOrDefault();
                if(department != null)
                {
                    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                    foreach(var w in department.Workers.ToList())
                    {
                        list.Add(workerService.GetWorkerInfo(w.Id));
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ChangeName(int departmentId, string newName)
        {
            using (var db = new DataContext())
            {
                if (db.Departments.Find(departmentId) != null)
                {
                    db.Departments.Find(departmentId).Name = newName;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
