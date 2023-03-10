using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
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
        /// Injecting Services
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
            Employee employee = _mapper.Map<Employee>(newEmployee);

            if (employee == null)
                return null;

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
        public async Task<GetEmployeeDto> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);

            if (employee == null)
            return null;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            var data = _mapper.Map<GetEmployeeDto>(employee);
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
            Employee dbEmployee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);

            if (dbEmployee == null)
            return null;

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
            var data = _mapper.Map<Employee>(updatedEmployee);
            _context.Employees.Update(data);
            await _context.SaveChangesAsync();
            return updatedEmployee;
        }

        public async Task<List<GetEmployeeDto>> SearchPeople(string search)
        {
            List<Employee> dbEmployee = await _context.Employees.ToListAsync();
            var data = _mapper.Map<List<GetEmployeeDto>>(dbEmployee.ToList());
            return data.Where(p => p.Name.ToLower().Contains(search.ToLower()) || p.EmployeeDept.ToLower().Contains(search.ToLower())).ToList();
        }

        public async Task<List<GetEmployeeDto>> GetSort(string sort)
        {
            List<Employee> dbEmployee = await _context.Employees.ToListAsync();
            var data = _mapper.Map<List<GetEmployeeDto>>(dbEmployee.ToList());
            switch (sort)
            {
                case "name_desc":
                    data = data.OrderByDescending(c => c.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(p => p.Name).ToList();
                    break;
            }
            return data;
        }
    }
}
