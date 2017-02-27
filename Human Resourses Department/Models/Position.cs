using System.Collections.Generic;

namespace Models
{
    public class Position
    {
        int id;
        string name;
        int hours;
        double payment;
        List<Worker> workers;

        public Position() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public double Payment { get; set; }
        public double TotalPayment
        {
            get
            {
                return hours * payment;
            }
        }
        public List<Worker> Workers { get; set; }
    }
}
