using System.Collections.Generic;

namespace Models
{
    public class Worker
    {
        int id;
        string name;
        string surname;
        string accountNumber;
        Department department;
        Position position;
        int experience;
        List<Project> projects;

        public Worker() { }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AccountNumber { get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }
        public int Experience { get; set; }
        public List<Project> Projects { get; set; }
        public int ProjectsCost
        {
            get
            {
                int cost = 0;
                foreach(var proj in projects)
                {
                    cost += proj.Cost;
                }
                return cost;
            }
        }
    }
}
