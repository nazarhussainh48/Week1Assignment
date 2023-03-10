using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
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
        public async Task<IActionResult> Get(int page = 1, int pageSize = 2)
        {
            try
            {
                var result = await _employeeService.GetAllEmployees();
                var totalRecords = result.Count();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                var items = result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
                return Ok(new { items }, MsgKeys.RetrieveEmployee);
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

                return Ok(new { result }, MsgKeys.RetrievSingle);
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
        public async Task<IActionResult> AddEmployee(GetEmployeeDto newEmployee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _employeeService.AddEmployee(newEmployee);
                return Ok(new { result }, MsgKeys.AddEmployee);
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
                var result = await _employeeService.UpdateEmployee(updatedEmployee);

                if (result == null)
                    return BadRequest(MsgKeys.InvalidUser);

                return Ok(new { result }, MsgKeys.UpdateMsg);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(id);

                if (result == null)
                    return BadRequest(MsgKeys.InvalidUser);

                return Ok(new { result }, MsgKeys.DeleteRecord);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(string search)
        {
            var results = await _employeeService.SearchPeople(search);

            return Ok(new { results }, "Your Search Result!");
        }

        [HttpGet("sort")]
        public async Task<IActionResult> Index(string sortOrder)
        {
            var results = await _employeeService.GetSort(sortOrder);
            return Ok(new { results }, "Your Search Result!");
        }
    }
}
