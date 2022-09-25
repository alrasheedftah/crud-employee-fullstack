using EmployeeCrudTaskAPi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.CrudDbContext
{
    public class CrudEmployeeDbContext : DbContext
    {
        public CrudEmployeeDbContext()
        {
        }

        public CrudEmployeeDbContext(DbContextOptions<CrudEmployeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
  }
