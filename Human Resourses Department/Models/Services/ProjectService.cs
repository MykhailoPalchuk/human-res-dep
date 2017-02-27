﻿using System.Collections.Generic;
using System.Linq;

namespace Models.Services
{
    public class ProjectService
    {
        public bool AddProject(string name, int cost)
        {
            using (var db = new DataContext())
            {
                foreach(var p in db.Projects.ToList())
                {
                    if (p.Name.Equals(name))
                        return false;
                }
                db.Projects.Add(new Project { Name = name, Cost = cost });
                db.SaveChanges();
                return true;
            }
        }

        public Dictionary<string, string> GetProjectInfo(int id)
        {
            using (var db = new DataContext())
            {
                Project p = db.Projects.Find(id);
                if (p != null)
                {
                    var proj = new Dictionary<string, string>();
                    proj.Add("id", p.Id.ToString());
                    proj.Add("name", p.Name);
                    proj.Add("cost", p.Cost.ToString());
                    return proj;
                }
                else
                    return null;
            }
        }

        public List<Dictionary<string,string>> GetAllProjectsInfo()
        {
            using (var db = new DataContext())
            {
                var list = new List<Dictionary<string, string>>();
                foreach (var p in db.Projects.ToList())
                {
                    list.Add(GetProjectInfo(p.Id));
                }
                return list;
            }
        }
    }
}
