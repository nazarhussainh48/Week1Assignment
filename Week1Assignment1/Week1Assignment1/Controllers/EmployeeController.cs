using Microsoft.AspNetCore.Mvc;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Services.EmployeeService;

namespace Week1Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : BaseController //(The ControllerBase class implements the IController interface and provides the implementation for several methods and properties)
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) // here we inject our new service into the controller by constructor
        {
            _employeeService = employeeService;
        }

        //Controller Methods are going here
        // to get all the routes
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetAllEmployees();
            return Ok(result, "All Employees are"); //get all employee is the service that is injected into this controller by Employee service interface
        }


        // Get Signle Employee
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            return Ok(result, "Success");
        }

        // Add Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto newEmployee)
        {
            var result = await _employeeService.AddEmployee(newEmployee);
            return Ok(result,"Emplyee Added Successfully");
        }

        // Update Employee
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            var result = await _employeeService.UpdateEmployee(updatedEmployee);
            if (result == null)
            {
                return BadRequest("User not found");

            }
            return Ok(result,"Updated Successfully");
        }

        /// <summary>
        /// Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An i action result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (result==null)
            {
                return BadRequest("User not found");
                
            }
            return Ok(result, "Data after deleting");
        }
    }
}
