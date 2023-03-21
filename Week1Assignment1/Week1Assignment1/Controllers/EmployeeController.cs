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
        private readonly IGenericRepository<Employee> _genericRepository;

        /// <summary>
        /// injecting employee service into the EmployeeController by constructor
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController(IEmployeeService employeeService, IGenericRepository<Employee> genericRepository)
        {
            _employeeService = employeeService;
            _genericRepository = genericRepository;
        }

        /// <summary>
        /// to get all employees
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("GetAll")]
        [HttpGet(Name = "EmployeeController"), Authorize]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _employeeService.GetAllEmployees(page, pageSize);

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

        /// <summary>
        /// Searching/Filtering
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        //public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> FilterEmployee(string name, string dept)
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> FilterEmployee(int id,string? name, string? dept, int salary, int age)
        {
            try
            {
                var result = await _employeeService.Filter(id, name, dept, salary, age);

                if (result.Count() == 0)
                    return BadRequest($"{name} of the department: {dept} not found");

                return Ok(new { result }, MsgKeys.FilterSuccess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Sorting
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        [HttpGet("sort")]
        public async Task<IActionResult> Sorting(string sortBy, string order)
        {
            try
            {
                var result = await _employeeService.GetSort(sortBy, order);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
