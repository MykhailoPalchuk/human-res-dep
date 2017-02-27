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
            return projectService.GetAllProjectsInfo();
        }
    }
}
