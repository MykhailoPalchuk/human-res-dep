using System.Collections.Generic;

namespace Models
{
    public class Position
    {
        public Position()
        {
            Workers = new List<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public double Payment { get; set; }
        public double TotalPayment
        {
            get
            {
                return Hours * Payment;
            }
        }
        public List<Worker> Workers { get; set; }
    }
}
