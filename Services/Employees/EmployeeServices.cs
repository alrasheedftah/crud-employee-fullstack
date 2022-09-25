using EmployeeCrudTaskAPi.CrudDbContext;
using EmployeeCrudTaskAPi.DBContext;
using EmployeeCrudTaskAPi.Helpers;
using EmployeeCrudTaskAPi.Models;
using EmployeeCrudTaskAPi.Resource.Requests;
using EmployeeCrudTaskAPi.Resource.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly CrudEmployeeDbContext _crudEmployeeDBContext;

        public EmployeeServices(CrudEmployeeDbContext crudEmployeeDbContext)
        {
            _crudEmployeeDBContext = crudEmployeeDbContext;
        }
        public async Task<SuccessResponse> CreateNew(Employee NewModel)
        {
            await _crudEmployeeDBContext.Employees.AddAsync(NewModel);
            await _crudEmployeeDBContext.SaveChangesAsync();

            return new SuccessResponse() {
                code = 1,
                MessageResult = "Emmployee Added ",
                Result = NewModel,
            };
        }

        public async Task<SuccessResponse> DeleteById(long EmployeeId)
        {
            var EmployeeExsits = await _crudEmployeeDBContext.Employees.FirstOrDefaultAsync(emp => emp.Id == EmployeeId);
            if(EmployeeExsits != null)
            {
                _crudEmployeeDBContext.Employees.Remove(EmployeeExsits);
                await _crudEmployeeDBContext.SaveChangesAsync();

                return new SuccessResponse()
                {
                    code = 1,
                    MessageResult = "Emmployee Deleted ",
                    Result = EmployeeExsits,
                };
            }

            else
                throw new HttpResponseException()
                {
                    Status = 401,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { " Employee Id Does Not Exsits " },
                        Code = 1
                    },
                };

        }

        public async Task<SuccessResponse> GetAll()
        {
            var EmployeesLists = await _crudEmployeeDBContext.Employees.ToListAsync();

            return new SuccessResponse() {
                Result = EmployeesLists,
                code = 1,
                MessageResult = "Employee Lists",
                Success = true
                
            };
                    
           
        }

        public async Task<SuccessResponse> GetById(long EmployeeId)
        {
            var EmployeeExsits = await _crudEmployeeDBContext.Employees.FirstOrDefaultAsync(emp => emp.Id == EmployeeId);

            if (EmployeeExsits != null)
            {
                return new SuccessResponse()
                {
                    code = 1,
                    MessageResult = "EmmployeeId Founded ",
                    Result = EmployeeExsits,
                };
            }

            else
                throw new HttpResponseException()
                {
                    Status = 401,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { " Employee Id Does Not Exsits " },
                        Code = 1
                    },
                };
        }

        public async Task<SuccessResponse> UpdateById(long EmployeeId, EmployeeRequest NewModel)
        {
            var EmployeeExsits = await _crudEmployeeDBContext.Employees.FirstOrDefaultAsync(emp => emp.Id == EmployeeId);
            
            if (EmployeeExsits != null)
            {
                EmployeeExsits.Address = NewModel.Address;
                EmployeeExsits.Degree = NewModel.Degree;
                EmployeeExsits.Department = NewModel.Department;
                EmployeeExsits.Name = NewModel.Name;
                EmployeeExsits.Phone = NewModel.PhoneNumber;
                EmployeeExsits.Salary = NewModel.EndSalary;
                _crudEmployeeDBContext.Employees.Update(EmployeeExsits);
                await _crudEmployeeDBContext.SaveChangesAsync();
                return new SuccessResponse()
                {
                    code = 1,
                    MessageResult = "EmmployeeId Updated  ",
                    Result = EmployeeExsits,
                };
            }

            else
                throw new HttpResponseException()
                {
                    Status = 401,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { " Employee Id Does Not Exsits " },
                        Code = 1
                    },
                };
        }
    }
}
