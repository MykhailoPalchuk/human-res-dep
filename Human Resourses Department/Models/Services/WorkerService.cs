using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Models.Services
{
    public class WorkerService
    {
        static ProjectService projectService = new ProjectService();

        public bool AddWorker(string name, string surname, string accountNumber,string departmentName,
            string positionName, int experience, string projectName)
        {
            using (var db = new DataContext())
            {
                //check if such worker exists
                var query = from w in db.Workers
                            where w.Name.Equals(name)
                            && w.Surname.Equals(surname)
                            select w;
                if (query.FirstOrDefault() != null)
                    return false;

                //find department
                Department dep = db.Departments.Where(p => p.Name.Equals(departmentName)).FirstOrDefault();
                if (dep == null)
                    return false;          //no such department

                //find position
                Position pos = db.Positions.Where(p => p.Name.Equals(positionName)).FirstOrDefault();
                if (pos == null)
                    return false;           //no such position

                //find project
                Project proj = db.Projects.Where(p => p.Name.Equals(projectName)).FirstOrDefault();
                if (proj == null)
                    return false;           //no such project

                //create worker
                db.Workers.Add(new Worker
                {
                    Name = name,
                    Surname = surname,
                    AccountNumber = accountNumber,
                    Department = dep,
                    Position = pos,
                    Experience = experience,
                    Projects = new List<Project>()
                });
                db.SaveChanges();

                //add project to a new worker
                var queryW = from w in db.Workers
                            where w.Name.Equals(name)
                            && w.Surname.Equals(surname)
                            select w;
                queryW.First().Projects.Add(proj);
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteWorker(int id)
        {
            using (var db = new DataContext())
            {
                if (db.Workers.Find(id) != null)
                {
                    db.Workers.Remove(db.Workers.Find(id));
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public Dictionary<string,string> GetWorkerInfo(int id)
        {
            using (var db = new DataContext())
            {
                Dictionary<string, string> worker = new Dictionary<string, string>();
                var list = db.Workers.Include(i => i.Department).Include(i => i.Position).Include(i => i.Projects);
                Worker w = list.Where(p => p.Id == id).FirstOrDefault();
                if (w != null)
                {
                    worker.Add("id", w.Id.ToString());
                    worker.Add("name", w.Name);
                    worker.Add("surname", w.Surname);
                    worker.Add("accountNumber", w.AccountNumber);
                    worker.Add("department", w.Department.Name);
                    worker.Add("position", w.Position.Name);
                    worker.Add("experience", w.Experience.ToString());
                    worker.Add("payment", w.Position.TotalPayment.ToString());
                    worker.Add("project", w.Projects.Last().Name);
                    worker.Add("projectsCost", w.ProjectsCost.ToString());
                    return worker;
                }
                else
                    return null;
            }
        }

        public List<Dictionary<string,string>> GetAllWorkersInfo()
        {
            using (var db = new DataContext())
            {
                var list = new List<Dictionary<string, string>>();
                foreach(var w in db.Workers.ToList())
                {
                    list.Add(GetWorkerInfo(w.Id));
                }
                return list;
            }
        }

        public bool ChangeName(int workerId, string newName)
        {
            using (var db = new DataContext())
            {
                Worker worker = db.Workers.Find(workerId);
                if (worker != null)
                {
                    worker.Name = newName;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ChangeSurname(int workerId, string newSurname)
        {
            using (var db = new DataContext())
            {
                Worker worker = db.Workers.Find(workerId);
                if (worker != null)
                {
                    worker.Surname = newSurname;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ChangeAccountNumber(int workerId, string newAccountNumber)
        {
            using (var db = new DataContext())
            {
                Worker worker = db.Workers.Find(workerId);
                if (worker != null)
                {
                    worker.AccountNumber = newAccountNumber;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ChangeDepartment(int workerId, string departmentName)
        {
            using (var db = new DataContext())
            {
                Worker worker = db.Workers.Find(workerId);
                if (worker != null)
                {
                    Department dep = null;
                    foreach(var d in db.Departments.ToList())
                    {
                        if (d.Name.Equals(departmentName))
                            dep = d;
                    }
                    if (dep != null)
                    {
                        worker.Department = dep;
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public bool ChangePosition(int workerId, string positionName)
        {
            using (var db = new DataContext())
            {
                Worker worker = db.Workers.Find(workerId);
                if (worker != null)
                {
                    Position pos = null;
                    foreach (var p in db.Positions.ToList())
                    {
                        if (p.Name.Equals(positionName))
                            pos = p;
                    }
                    if (pos != null)
                    {
                        worker.Position = pos;
                        db.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public bool ChangeExperience(int workerId, int newExperience)
        {
            using (var db = new DataContext())
            {
                Worker worker = db.Workers.Find(workerId);
                if (worker != null)
                {
                    worker.Experience = newExperience;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool AddProject(int workerId, string projectName)
        {
            using (var db = new DataContext())
            {
                Project proj = null;
                foreach(var p in db.Projects.ToList())
                {
                    if (p.Name.Equals(projectName))
                        proj = p;
                }
                if (proj == null)
                    return false;
                else
                {
                    Worker w = db.Workers.Find(workerId);
                    if (w == null)
                        return false;
                    else
                    {
                        w.Projects.Add(proj);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
        }

        public List<Dictionary<string, string>> GetWorkersProjects(int workerId)
        {
            using (var db = new DataContext())
            {
                Worker w = db.Workers.Find(workerId);
                if (w != null)
                {
                    var list = new List<Dictionary<string, string>>();
                    foreach (var p in w.Projects)
                    {
                        list.Add(projectService.GetProjectInfo(p.Id));
                    }
                    return list;
                }
                else
                    return null;
            }
        }
    }
}
