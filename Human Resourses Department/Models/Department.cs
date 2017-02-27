using System.Collections.Generic;

namespace Models
{
    public class Department
    {
        int id;
        string name;
        List<Worker> workers;

        public Department() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
