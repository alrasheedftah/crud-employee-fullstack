using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Resource.Requests
{
    public class EmployeeRequest
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Degree { get; set; } 
    }
}
