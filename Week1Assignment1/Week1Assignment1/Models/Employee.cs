using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week1Assignment1.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmployeeDept { get; set; }
        [Required]
        public int Salary { get; set; }

        [Range(18, 130)]
        [Required]
        public int Age { get; set; }
        [Required]
        public EmpEnum Class { get; set; } = EmpEnum.Nazar;

    }
}
