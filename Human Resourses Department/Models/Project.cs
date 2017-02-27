
using System.Collections.Generic;

namespace Models
{
    public class Project
    {
        public Project()
        {
            Workers = new List<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public List<Worker> Workers { get; set; }
        public int NumberOfWorkers
        {
            get
            {
                return Workers.Count;
            }
        }
    }
}
