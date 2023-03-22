using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Repository;
using Week1Assignment1.Services.EmployeeService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Week1Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

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
        [HttpGet(Name = "EmployeeController"), Authorize]
        public ActionResult<IEnumerable<GetEmployeeDto>> Get([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var result =  _employeeService.GetAllEmployees(queryParameters);

                if (result == null)
                    return BadRequest(MsgKeys.NoEmployeeFound);

                return Ok(new { result }, MsgKeys.RetrieveEmployee);
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
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(id);

                if (result == null)
                    return BadRequest(MsgKeys.InvalidUser);

                return Ok(result, MsgKeys.RetrievSingle);
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
        public async Task<IActionResult> AddEmployees(GetEmployeeDto newEmployee)
        {
            try
            {
                if (!ModelState.IsValid || newEmployee == null)
                    return BadRequest(MsgKeys.NullData);

                var result = await _employeeService.AddEmployee(newEmployee);
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
        public async Task<IActionResult> UpdateEmployee(GetEmployeeDto updatedEmployee)
        {
            try
            {
                if (!ModelState.IsValid || updatedEmployee == null)
                    return BadRequest(MsgKeys.NullData);

                var result = await _employeeService.UpdateEmployee(updatedEmployee);
                return Ok(result, MsgKeys.UpdateMsg);
            }
            catch
            {
                return BadRequest(MsgKeys.UserNotFound);
            }
        }

        /// <summary>
        /// Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(id);
                return Ok(MsgKeys.DeleteRecord);
            }
            catch
            {
                return BadRequest(MsgKeys.UserNotFound);
            }
        } 
    }
}
