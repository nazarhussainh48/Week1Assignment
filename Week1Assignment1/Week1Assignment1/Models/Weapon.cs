namespace Week1Assignment1.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Damage { get; set; }
        public Employee Employee { get; set; } //Used for the one to many or one to one relation 
        public int EmployeeId { get; set; }

        
    }
}
