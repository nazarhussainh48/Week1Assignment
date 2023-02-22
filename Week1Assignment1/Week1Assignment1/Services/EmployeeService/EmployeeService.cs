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
        public EmployeeService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            ServiceResponse<List<GetEmployeeDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            Employee employee = _mapper.Map<Employee>(newEmployee);
            employee.Id = employees.Max(c => c.Id) + 1;
            employees.Add(employee);
            serviceResponse.Data = (employees.Select(c => _mapper.Map<GetEmployeeDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id)
        {
            ServiceResponse<List<GetEmployeeDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            try
            {
                Employee employee = employees.First(c => c.Id == id);
                employees.Remove(employee);
                serviceResponse.Data = (employees.Select(c => _mapper.Map<GetEmployeeDto>(c))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }


            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployees()
        {
            ServiceResponse<List<GetEmployeeDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            serviceResponse.Data = (employees.Select(c => _mapper.Map<GetEmployeeDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id)
        {
            ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();

            try { 
            serviceResponse.Data = _mapper.Map<GetEmployeeDto>(employees.First(c => c.Id == id));
            }
            catch (Exception ex) 
            {
                serviceResponse.Message = ex.Message;   
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();

            try
            {
                Employee employee = employees.FirstOrDefault(c => c.Id == updatedEmployee.Id);
                employee.Name = updatedEmployee.Name;
                employee.EmployeeDept = updatedEmployee.EmployeeDept;
                employee.Salary = updatedEmployee.Salary;
                employee.Age = updatedEmployee.Age;
                serviceResponse.Data = _mapper.Map<GetEmployeeDto>(employee);
            }
            catch(Exception ex) 
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            

            return serviceResponse;

        }
    }
}
