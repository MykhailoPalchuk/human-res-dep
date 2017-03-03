using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Tables
{
    public class WorkerTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AccountNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string Experience { get; set; }
        public string LastProject { get; set; }
        public string ProjectCost { get; set; }

        public WorkerTable()
        {
            Id = "Id";
            Name = "Name";
            Surname = "Surname";
            AccountNumber = "Account number";
            DepartmentName = "Department";
            PositionName = "Position";
            Experience = "Experience";
            LastProject = "Last project";
            ProjectCost = "Total project cost";
        }

        public WorkerTable(string id, string name, string surname, string accountNumber,
            string departmentName, string positionName, string experience, string lastProject, string projectsCost)
        {
            Id = id;
            Name = name;
            Surname = surname;
            AccountNumber = accountNumber;
            DepartmentName = departmentName;
            PositionName = positionName;
            Experience = experience;
            LastProject = lastProject;
            ProjectCost = projectsCost;
        }
    }
}
