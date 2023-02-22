using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.EmployeeService
{
    public interface IEmployeeService
    {
        //For Getting List of all employees
        Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployees();

        //For Getting the list of single employee by id
        Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id);

        //For adding employee 
        Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee);
        
        //For Updating Employee
        Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updatedEmployee);

        //For Deleting Employee
        Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id); 
    }
}
