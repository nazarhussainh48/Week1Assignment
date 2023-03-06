using System.ComponentModel.DataAnnotations;
using Week1Assignment1.DTO.Weapon;
using Week1Assignment1.Models;

namespace Week1Assignment1.DTO.Employee
{

    /// <summary>
    /// Data Transfer Object To Get Employee
    /// </summary>
    public class GetEmployeeDto
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

        public GetWeaponDto Weapon { get; set; }
    }
}
