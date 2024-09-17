using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.EmployeeService
{
    public interface IEmployeeService
    {
        /// <summary>
        /// For Getting List of all employees
        /// </summary>
        /// <returns></returns>
        List<GetEmployeeDto> GetAllEmployees();

        /// <summary>
        /// For Getting the list of single employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetEmployeeDto GetEmployeeById(int id);

        /// <summary>
        /// For adding employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        List<GetEmployeeDto> AddEmployee(GetEmployeeDto newEmployee);

        /// <summary>
        /// For Updating Employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns></returns>
        GetEmployeeDto UpdateEmployee(GetEmployeeDto updatedEmployee);

        /// <summary>
        /// For Deleting Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<GetEmployeeDto> DeleteEmployee(int id);
    }
}
