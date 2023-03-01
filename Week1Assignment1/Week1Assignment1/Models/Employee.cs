namespace Week1Assignment1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Nazar";
        public string EmployeeDept { get; set; } = "Development";
        public int Salary { get; set; } = 30000;
        public int Age { get; set; } = 25;
        public EmpEnum Class { get; set; } = EmpEnum.Nazar;
    }
}
