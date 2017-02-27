using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Tables
{
    class PositionTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Hours { get; set; }
        public string Payment { get; set; }
        public string TotalPayment { get; set; }

        public PositionTable()
        {
            Id = "Id";
            Name = "Name";
            Hours = "Hours";
            Payment = "Payment";
            TotalPayment = "Total payment";
        }

        public PositionTable(string id, string name, string hours, string payment, string totalPayment)
        {
            Id = id;
            Name = name;
            Hours = hours;
            Payment = payment;
            TotalPayment = totalPayment;
        }
    }
}
