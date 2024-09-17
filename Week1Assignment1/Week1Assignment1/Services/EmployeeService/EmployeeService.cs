using AutoMapper;
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
        public EmployeeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns>List of Employees</returns>
        public List<GetEmployeeDto> AddEmployee(GetEmployeeDto newEmployee)
        {
            var employee = _mapper.Map<Employee>(newEmployee);

            if (employee == null)
            {
                return null;
            }

            if (employees.Count == 0)
            {
                employee.Id = 0;
            }
            else
            {
                employee.Id = employees.Max(c => c.Id) + 1;
            }

            employees.Add(employee);
            var data = _mapper.Map<List<GetEmployeeDto>>(employees.ToList());
            return data;
        }

        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GetEmployeeDto> DeleteEmployee(int id)
        {

            var employee = employees.FirstOrDefault(c => c.Id == id);

            if (employee == null)
            {
                return null;
            }

            employees.Remove(employee);
            var data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return data;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        public List<GetEmployeeDto> GetAllEmployees()
        {
            var data = _mapper.Map<List<GetEmployeeDto>>(employees.ToList());
            return data;
        }

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetEmployeeDto GetEmployeeById(int id)
        {
            var employee = employees.FirstOrDefault(c => c.Id == id);

            if (employee == null)
            {
                return null;
            }

            var data = _mapper.Map<GetEmployeeDto>(employees.First(c => c.Id == id));
            return data;
        }

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns></returns>
        public GetEmployeeDto UpdateEmployee(GetEmployeeDto updatedEmployee)
        {
            var employee = employees.FirstOrDefault(c => c.Id == updatedEmployee.Id);

            if (employee == null)
            {
                return null;
            }

            employee.Name = updatedEmployee.Name;
            employee.EmployeeDept = updatedEmployee.EmployeeDept;
            employee.Salary = updatedEmployee.Salary;
            employee.Age = updatedEmployee.Age;
            var data = _mapper.Map<GetEmployeeDto>(employee);
            return data;

        }
    }
}
