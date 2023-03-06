namespace Week1Assignment1.Models
{
    public class EmployeeSkill
    {
        /// <summary>
        /// for many to many relation
        /// </summary>
        /// 
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
