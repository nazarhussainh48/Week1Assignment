using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
using System.Security.Claims;
using Week1Assignment1.Data;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Repository;

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
            //new Employee {Id=1, Name="Malik"}
        };

        /// <summary>
        /// Injecting Services
        /// </summary>
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IGenericRepository<Employee> _genericRepository;

        public EmployeeService(IMapper mapper, DataContext context, IGenericRepository<Employee> genericRepository)
        {
            _mapper = mapper;
            _context = context;
            _genericRepository = genericRepository;
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns>List of Employees</returns>
        public async Task<GetEmployeeDto> AddEmployee(GetEmployeeDto newEmployee)
        {
            var model = _mapper.Map<Employee>(newEmployee);
            var result = await _genericRepository.Insert(model);
            var model1 = _mapper.Map<GetEmployeeDto>(result);
            return model1;
        }

        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> DeleteEmployee(int id)
        {;
            var data = await _genericRepository.Delete(id);
            var model1 = _mapper.Map<GetEmployeeDto>(data);
            return model1;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetEmployeeDto>> GetAllEmployees(int page, int pageSize)
        {
            var result1 = _genericRepository.GetAll(page, pageSize);
            var result = _mapper.Map<List<GetEmployeeDto>>(result1);
            return result;
        }

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> GetEmployeeById(int id)
        {
            //Employee dbEmployee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
            var result = _genericRepository.GetSingle(id);

            if (result == null)
                return null;

            var data = _mapper.Map<GetEmployeeDto>(result);
            return data;
        }

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> UpdateEmployee(GetEmployeeDto updatedEmployee)
        {
            var data = _mapper.Map<Employee>(_genericRepository.GetSingle(updatedEmployee.Id));

            if (data == null)
            {
                throw new Exception(MsgKeys.NullData);
            }

            data.Name = updatedEmployee.Name;
            data.EmployeeDept = updatedEmployee.EmployeeDept;
            data.Salary = updatedEmployee.Salary;
            await _genericRepository.Update(data);
            var model1 = _mapper.Map<GetEmployeeDto>(_genericRepository.GetSingle(updatedEmployee.Id));
            return model1;
        }

        /// <summary>
        /// Search People
        /// </summary>
        /// <param name="search"></param>
        /// <param name="employeeDept"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetEmployeeDto>> Filter(int id,string? name, string? dept, int salary, int age)
        {
            //var data1 = await _genericRepository.Filter(p => p.Id==id || p.Name == name || p.EmployeeDept == dept || p.Salary==salary || p.Age==age);
            var data = await _genericRepository.Filter(p =>p.Id==id || p.Name.Contains(name) || p.EmployeeDept.Contains(dept) || p.Salary <= salary || p.Age <= age);
            var model1 = _mapper.Map<IEnumerable<GetEmployeeDto>>(data);
            return model1;
        }

        /// <summary>
        /// Sorting
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetEmployeeDto>> GetSort(string sortBy, string order)
        {
            var result = await _genericRepository.Sorting(sortBy, order);
            var model1 = _mapper.Map<IEnumerable<GetEmployeeDto>>(result);
            return model1;
        }
    }
}
