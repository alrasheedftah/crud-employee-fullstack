using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Degree { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }

    }
}
