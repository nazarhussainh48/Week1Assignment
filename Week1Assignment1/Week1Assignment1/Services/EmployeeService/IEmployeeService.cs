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
        Task<List<GetEmployeeDto>> GetAllEmployees(int page, int pageSize);

        /// <summary>
        /// For Getting the list of single employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetEmployeeDto> GetEmployeeById(int id);

        /// <summary>
        /// For adding employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        //Task<List<GetEmployeeDto>> AddEmployee(GetEmployeeDto newEmployee);
        Task<GetEmployeeDto> AddEmployee(GetEmployeeDto newEmployee);

        /// <summary>
        /// For Updating Employee
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns></returns>
        Task<GetEmployeeDto> UpdateEmployee(GetEmployeeDto updatedEmployee);

        /// <summary>
        /// For Deleting Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetEmployeeDto> DeleteEmployee(int id);

        Task<IEnumerable<GetEmployeeDto>> Filter(int id, string? name, string? dept, int salary, int age);

        Task<IEnumerable<GetEmployeeDto>> GetSort(string sortBy, string order);
    }
}
