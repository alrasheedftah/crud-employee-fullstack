using EmployeeCrudTaskAPi.Controllers.Employees;
using EmployeeCrudTaskAPi.CrudDbContext;
using EmployeeCrudTaskAPi.Models;
using EmployeeCrudTaskAPi.Resource.Responses;
using EmployeeCrudTaskAPi.Services.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using EmployeeCrudTaskAPi.Resource.Requests;

namespace TestApiTasks
{
    public class UnitTest1
    {



        EmployeeController _employeeController;




        [Fact]
        public async Task Should_add_New_EmployeeAsync()
        {
            //OK RESULT TEST START
            //create In Memory Database
            var options = new DbContextOptionsBuilder<CrudEmployeeDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;


            using (var context = new CrudEmployeeDbContext(options))
            {

                //Arrange
                var newEmployee = new EmployeeRequest()
                {
                    Degree = "USA",
                    Department = "NY",
                    Address = "Test1",
                    Name = "121312",
                    Phone = "000000",
                    Salary = 2222,
                };

                //Act
                _employeeController = new EmployeeController(new EmployeeServices(context));
                var createdResponse = await _employeeController.store(newEmployee) as Microsoft.AspNetCore.Mvc.ObjectResult;

                //Assert
                Assert.IsType<SuccessResponse>(createdResponse.Value);

                //value of the result
                var item = createdResponse.Value as SuccessResponse;
                Assert.IsType<Employee>(item.Result);

                //check value of this book
                var bookItem = item.Result as Employee;
                Assert.Equal(newEmployee.Name, bookItem.Name);
                Assert.Equal(newEmployee.Department, bookItem.Department);
                Assert.Equal(newEmployee.Address, bookItem.Address);

                //OK RESULT TEST END

                //BADREQUEST AND MODELSTATE ERROR TEST START

                //Arrange
                var incompleteEmployee = new EmployeeRequest()
                {
                };


                // TODO Add Validation 
                ////Act
                //_employeeController.ModelState.AddModelError("Name", "Title is a requried filed");
                //var badResponse = _employeeController.store(incompleteEmployee);

                ////Assert
                //Assert.IsType<BadRequestObjectResult>(badResponse);
            }

            //BADREQUEST AND MODELSTATE ERROR TEST END
        }


        [Fact]
        public async Task Should_Update_Old_EmployeeWith_NewDataAsync()
        {
            //OK RESULT TEST START
            //create In Memory Database
            var options = new DbContextOptionsBuilder<CrudEmployeeDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;


            using (var context = new CrudEmployeeDbContext(options))
            {

                //Arrange
                var newEmployee = new EmployeeRequest()
                {
                    Degree = "USA",
                    Department = "NY",
                    Address = "Test1",
                    Name = "Alrasheed",
                    Phone = "000000",
                    Salary = 2222,
                };

                var EmployID = 2;
                //Act
                _employeeController = new EmployeeController(new EmployeeServices(context));
                var createdResponse = await _employeeController.update(EmployID,newEmployee) as Microsoft.AspNetCore.Mvc.ObjectResult;

                //Assert
                Assert.IsType<SuccessResponse>(createdResponse.Value);

                //value of the result
                var item = createdResponse.Value as SuccessResponse;
                Assert.IsType<Employee>(item.Result);

                //check value of this book
                var empItem = item.Result as Employee;
                Assert.Equal(newEmployee.Name, empItem.Name);
                
                //BADREQUEST AND MODELSTATE ERROR TEST START

                //Arrange
                var incompleteEmployee = new EmployeeRequest()
                {
                };


                // TODO Add Validation 
                ////Act
                //_employeeController.ModelState.AddModelError("Name", "Title is a requried filed");
                //var badResponse = _employeeController.store(incompleteEmployee);

                ////Assert
                //Assert.IsType<BadRequestObjectResult>(badResponse);
            }

            //BADREQUEST AND MODELSTATE ERROR TEST END
        }

        [Fact]
        public async Task should_return_list_of_employee_count_2()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<CrudEmployeeDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            using (var context = new CrudEmployeeDbContext(options))
            {
                context.Employees.Add(new Employee
                {
                    Id = 1,
                    Degree = "USA",
                    Department = "NY",
                    Address = "Test1",
                    Name = "121312"
                }
                
                );

                context.Employees.Add(new Employee
                {
                    Id = 2,
                    Degree = "USA",
                    Department = "NY",
                    Address = "Test1",
                    Name = "121312"
                }

                );

                context.SaveChanges();
            }


            // Use a Context instance  with Data to run the test for your Business code 

            using (var context = new CrudEmployeeDbContext(options))
            {
                _employeeController = new EmployeeController(new EmployeeServices(context));
                var result = await _employeeController.index() as Microsoft.AspNetCore.Mvc.ObjectResult;

                Assert.IsType<SuccessResponse>(result.Value);

                var list = result.Value as SuccessResponse;

                Assert.IsType<List<Employee>>(list.Result);

                var listBooks = list.Result as List<Employee>;

                Assert.Equal(2, listBooks.Count);
            }


        }


        [Fact]
        public async Task should_return_One_of_employee_count_ById()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<CrudEmployeeDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            // Use a Context instance  with Data to run the test for your Business code 

            using (var context = new CrudEmployeeDbContext(options))
            {
                var SearchId = 2;
                _employeeController = new EmployeeController(new EmployeeServices(context));
                var result = await _employeeController.show(SearchId) as Microsoft.AspNetCore.Mvc.ObjectResult;

                Assert.IsType<SuccessResponse>(result.Value);

                var list = result.Value as SuccessResponse;

                Assert.IsType<Employee>(list.Result);

                var employeeOne = list.Result as Employee;

                Assert.Equal(2, employeeOne.Id );
            }


        }


    }
}
