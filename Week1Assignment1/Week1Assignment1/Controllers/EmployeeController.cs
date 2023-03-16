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
        public IActionResult Get(int page = 1, int pageSize = 2)
        {
            try
            {
                var result = _genericRepository.GetAll();
                var totalRecords = result.Count();

                if (totalRecords <= 0)
                    return NotFound();

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
                var result = _genericRepository.GetSingle(id);

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
        public async Task<IActionResult> AddEmployee(Employee newEmployee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _genericRepository.Insert(newEmployee);
                var result = _genericRepository.SaveChanges();
                if (result > 0)
                    return Ok(new { newEmployee }, MsgKeys.AddEmployee);

                return BadRequest("failed to add user");
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
        public IActionResult UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
                _genericRepository.Update(updatedEmployee);
                var result = _genericRepository.SaveChanges();
                return Ok(new { updatedEmployee }, MsgKeys.UpdateMsg);
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
                var result = _genericRepository.GetSingle(id);
                _genericRepository.Delete(result);
                _genericRepository.SaveChanges();
                return Ok(new { result }, MsgKeys.DeleteRecord);
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
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetProductsByCategory(string name, string dept)
        {
            try
            {
                var result = await _genericRepository.GetBy(p => p.Name == name && p.EmployeeDept == dept);

                if (result.Count() == 0)
                    return BadRequest($"{name} of the department: {dept} not found");

                return Ok(new { result });
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
        public async Task<IActionResult> Index(string sortBy)
        {
            try
            {
                var result = await _genericRepository.GetAll(x => x.Name, sortBy);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
