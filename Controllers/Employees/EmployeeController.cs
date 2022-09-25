using EmployeeCrudTaskAPi.Helpers;
using EmployeeCrudTaskAPi.Models;
using EmployeeCrudTaskAPi.Resource.Requests;
using EmployeeCrudTaskAPi.Routes;
using EmployeeCrudTaskAPi.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Controllers.Employees
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpGet(ApiRoutes.EmployRoute.Employees)]
        public async Task<IActionResult> index()
        {
            var EmployeeData = await _employeeServices.GetAll();
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorsResponse
                    {
                        Errors = new[] { "SomeThing Wrong" },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }

        [HttpGet(ApiRoutes.EmployRoute.Employees + "/{EmployeeId}")]
        public async Task<IActionResult> show([FromRoute] int EmployeeId)
        {
            var EmployeeData = await _employeeServices.GetById(EmployeeId);
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorsResponse
                    {
                        Errors = new[] { "The Company Id Not Found " },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }

        [HttpPost(ApiRoutes.EmployRoute.Employees)]
        public async Task<IActionResult> store(EmployeeRequest NewModel)
        {

            var EmployeeData = await _employeeServices.CreateNew(new Employee()
            {
                Address = NewModel.Address,
                Degree = NewModel.Degree,
                Department = NewModel.Department,
                Name = NewModel.Name,
                Phone = NewModel.Phone,
                Salary = NewModel.Salary,
            });
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorsResponse
                    {
                        Errors = new[] { "There SomeThing Wrong" },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }


        [HttpPut(ApiRoutes.EmployRoute.Employees + "/{EmployeeId}")]
        public async Task<IActionResult> update([FromRoute] int EmployeeId,EmployeeRequest NewModel)
        {
            
            var EmployeeData = await _employeeServices.UpdateById(EmployeeId,NewModel);
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorsResponse
                    {
                        Errors = new[] { "There SomeThing Wrong" },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }


        [HttpDelete(ApiRoutes.EmployRoute.Employees + "/{EmployeeId}")]
        public async Task<IActionResult> delete([FromRoute] int EmployeeId)
        {
            var EmployeeData = await _employeeServices.DeleteById(EmployeeId);
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorsResponse
                    {
                        Errors = new[] { "There SomeThing Wrong" },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }



    }
 }
