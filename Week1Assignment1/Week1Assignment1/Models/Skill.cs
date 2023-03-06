namespace Week1Assignment1.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Damage { get; set; }
        public List<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
