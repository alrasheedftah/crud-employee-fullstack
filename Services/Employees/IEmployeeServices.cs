using EmployeeCrudTaskAPi.Models;
using EmployeeCrudTaskAPi.Resource.Requests;
using EmployeeCrudTaskAPi.Resource.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Services.Employees
{
    public interface IEmployeeServices
    {
        Task<SuccessResponse> GetAll();
        Task<SuccessResponse> GetById(long EmployeeId);
        Task<SuccessResponse> CreateNew(Employee NewModel);
        Task<SuccessResponse> UpdateById(long EmployeeId,EmployeeRequest NewModel);
        Task<SuccessResponse> DeleteById(long EmployeeId);
    }
}
