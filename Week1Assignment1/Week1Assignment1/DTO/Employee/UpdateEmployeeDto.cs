using Week1Assignment1.Models;

namespace Week1Assignment1.DTO.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Nazar";
        public string EmployeeDept { get; set; } = "Backend";
        public int Salary { get; set; } = 20000;
        public int Age { get; set; } = 23;
        public EmpEnum Class { get; set; } = EmpEnum.Nazar;
    }
}
