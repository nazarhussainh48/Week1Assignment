using Week1Assignment1.Models;

namespace Week1Assignment1.DTO.Employee
{

    /// <summary>
    /// Data Transfer Object To Get Employee
    /// </summary>
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string EmployeeDept { get; set; } 
        public int Salary { get; set; } 
        public int Age { get; set; } 
        public EmpEnum Class { get; set; } = EmpEnum.Nazar;
    }
}
