using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Week1Assignment1.Data;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// List of employees
        /// </summary>
        private static readonly List<Employee> employees = new List<Employee>
        {
            new Employee(),
            new Employee {Id=1, Name="Malik"}
        };

        /// <summary>
        /// Automapper Injection 
        /// </summary>
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmployeeService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns>List of Employees</returns>
        public async Task<List<GetEmployeeDto>> AddEmployee(GetEmployeeDto newEmployee)
        {
            var employee = _mapper.Map<Employee>(newEmployee);

            if (employee == null)
            {
                return null;
            }

            //if (employees.Count == 0)
            //{
            //    employee.Id = 0;
            //}
            //else
            //{
            //    employee.Id = employees.Max(c => c.Id) + 1;
            //}

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            var data = _mapper.Map<List<GetEmployeeDto>>(_context.Employees.ToList());
            return data;
        }

        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<GetEmployeeDto>> DeleteEmployee(int id)
        {

            var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);

            if (employee == null)
            {
                return null;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            var data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return data;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            List<Employee> dbEmployee = await _context.Employees.ToListAsync();

            var data = _mapper.Map<List<GetEmployeeDto>>(dbEmployee.ToList());
            return data;
        }

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> GetEmployeeById(int id)
        {

            //var employee = employees.FirstOrDefault(c => c.Id == id);
            Employee? dbEmployee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);

            if (dbEmployee == null)
            {
                return null;
            }

            var data = _mapper.Map<GetEmployeeDto>(dbEmployee);
            return data;
        }

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> UpdateEmployee(GetEmployeeDto updatedEmployee)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == updatedEmployee.Id);

            if (employee == null)
            {
                return null;
            }

            employee.Name = updatedEmployee.Name;
            employee.EmployeeDept = updatedEmployee.EmployeeDept;
            employee.Salary = updatedEmployee.Salary;
            employee.Age = updatedEmployee.Age;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            var data = _mapper.Map<GetEmployeeDto>(employee);
            return data;

        }
    }
}
