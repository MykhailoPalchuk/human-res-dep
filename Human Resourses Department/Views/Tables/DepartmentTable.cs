namespace Views.Tables
{
    public class DepartmentTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NumberOfWorkers { get; set; }

        public DepartmentTable()
        {
            Id = "Id";
            Name = "Name";
            NumberOfWorkers = "Number of workers";
        }

        public DepartmentTable(string id, string name, string numberOfWorkers)
        {
            Id = id;
            Name = name;
            NumberOfWorkers = numberOfWorkers;
        }
    }
}
