using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Tables
{
    public class ProjectTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string NumberOfWorkers { get; set; }

        public ProjectTable()
        {
            Id = "Id";
            Name = "Name";
            Cost = "Cost";
            NumberOfWorkers = "Number of workers";
        }

        public ProjectTable(string id, string name, string cost, string numberOfWorkers)
        {
            Id = id;
            Name = name;
            Cost = cost;
            NumberOfWorkers = numberOfWorkers;
        }
    }
}
