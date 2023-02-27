using AutoMapper;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly List<Employee> employees = new List<Employee>  //for the list
        {
            new Employee(),
            new Employee {Id=1, Name="Malik"}
        };

        private readonly IMapper _mapper;
        public EmployeeService(IMapper mapper) //Imapper is the interface of automapper
        {
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeDto>> AddEmployee(AddEmployeeDto newEmployee)
        {
            //ServiceResponse<List<GetEmployeeDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            // Id increment directly in Model Employee

            Employee employee = _mapper.Map<Employee>(newEmployee);
            if (employees.Count == 0)
            {
                employee.Id = 0;
            }
            else
            {
                employee.Id = employees.Max(c => c.Id) + 1;
            }

            employees.Add(employee);


            //    serviceResponse.Data = (employees.Select(c => _mapper.Map<GetEmployeeDto>(c))).ToList();
            var data = _mapper.Map<List<GetEmployeeDto>>(employees.ToList());
            return data;
        }

        public async Task<List<GetEmployeeDto>> DeleteEmployee(int id)
        {
            //ServiceResponse<List<GetEmployeeDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            Employee employee = employees.FirstOrDefault(c => c.Id == id);
            if (employee == null)
            {
                return null;
            }
            employees.Remove(employee);
            var data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return data;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            //ServiceResponse<List<GetEmployeeDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            var data = _mapper.Map<List<GetEmployeeDto>>(employees.ToList());
            return data;
        }

        public async Task<GetEmployeeDto> GetEmployeeById(int id)
        {
            //ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();

            var data = _mapper.Map<GetEmployeeDto>(employees.First(c => c.Id == id));
            return data;
        }

        public async Task<GetEmployeeDto> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            //ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();

            Employee employee = employees.FirstOrDefault(c => c.Id == updatedEmployee.Id);
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
