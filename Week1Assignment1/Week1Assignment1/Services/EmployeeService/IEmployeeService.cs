using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.EmployeeService
{
    public interface IEmployeeService
    {
        //For Getting List of all employees
        Task<List<GetEmployeeDto>> GetAllEmployees();

        //For Getting the list of single employee by id
        Task<GetEmployeeDto> GetEmployeeById(int id);

        //For adding employee
        Task<List<GetEmployeeDto>> AddEmployee(AddEmployeeDto newEmployee);
        
        //For Updating Employee
        Task<GetEmployeeDto> UpdateEmployee(UpdateEmployeeDto updatedEmployee);

        //For Deleting Employee
        Task<List<GetEmployeeDto>> DeleteEmployee(int id); 
    }
}
