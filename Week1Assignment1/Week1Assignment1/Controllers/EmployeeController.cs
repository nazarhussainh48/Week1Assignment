using Microsoft.AspNetCore.Mvc;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;
using Week1Assignment1.Services.EmployeeService;

namespace Week1Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase //(The ControllerBase class implements the IController interface and provides the implementation for several methods and properties)
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
            return Ok(await _employeeService.GetAllEmployees());
        }

        // Get Signle Employee
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try { 
            return Ok(await _employeeService.GetEmployeeById(id));
            }
            catch 
            {
                return NotFound("record of the id: " + id + " not found");
            }
        }

        // Add Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto newEmployee)
        {
            return Ok(await _employeeService.AddEmployee(newEmployee));
        }

        // Update Employee
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            ServiceResponse<GetEmployeeDto> response = await _employeeService.UpdateEmployee(updatedEmployee);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
                ServiceResponse<List<GetEmployeeDto>> response = await _employeeService.DeleteEmployee(id);               
                return Ok(response);    
        }
    }
}
