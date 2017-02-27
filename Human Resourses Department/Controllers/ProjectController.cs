using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;

namespace Controllers
{
    public class ProjectController
    {
        static ProjectService projectService = new ProjectService();

        public bool AddProject(string name, int cost)
        {
            return projectService.AddProject(name, cost);
        }

        public bool DeleteProject(int id)
        {
            return projectService.DeleteProject(id);
        }

        public List<Dictionary<string,string>> GetAllProjectsInfo()
        {
            var list = projectService.GetAllProjectsInfo();
            list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string a, b;
                d1.TryGetValue("name", out a);
                d2.TryGetValue("name", out b);
                return a.CompareTo(b);
            });
            return list;
        }
    }
}
