using System.ComponentModel.DataAnnotations;
using Week1Assignment1.Models;

namespace Week1Assignment1.DTO.Employee
{

    /// <summary>
    /// Data Transfer Object To Get Employee
    /// </summary>
    public class GetEmployeeDto
    {
        public int Id { get; set; }

        //[StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        
        public string EmployeeDept { get; set; }

        public int Salary { get; set; }

        //[Range(18, 130)]
        
        public int Age { get; set; }
        
        public EmpEnum Class { get; set; } = EmpEnum.Nazar;
    }
}
