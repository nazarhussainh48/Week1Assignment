using Microsoft.AspNetCore.Mvc;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Services.EmployeeService;

namespace Week1Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EmployeeController : BaseController
    {
        /// <summary>
        /// The Employee Controller
        /// </summary>
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// injecting employee service into the EmployeeController by constructor
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// to get all employees
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var result = _employeeService.GetAllEmployees();
                return Ok(result, MsgKeys.RetrieveEmployee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Signle Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            try
            {
                var result = _employeeService.GetEmployeeById(id);

                if (result == null)
                    return BadRequest(MsgKeys.InvalidUser);

                return Ok(result, MsgKeys.Success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult AddEmployee(GetEmployeeDto newEmployee)
        {
            try
            {

                if (newEmployee.Name =="")
                {
                    return BadRequest("Please Enter the Name");
                }
                else if (newEmployee.EmployeeDept == "")
                {
                    return BadRequest("Please enter the Department");
                }

                var result =  _employeeService.AddEmployee(newEmployee);
                return Ok(result, MsgKeys.AddEmployee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        public IActionResult UpdateEmployee(GetEmployeeDto updatedEmployee)
        {
            try
            {
                var result = _employeeService.UpdateEmployee(updatedEmployee);

                if (result == null)
                    return BadRequest(MsgKeys.InvalidUser);

                return Ok(result, MsgKeys.UpdateMsg);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _employeeService.DeleteEmployee(id);

                if (result == null)
                    return BadRequest(MsgKeys.InvalidUser);

                return Ok(result, MsgKeys.Success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
