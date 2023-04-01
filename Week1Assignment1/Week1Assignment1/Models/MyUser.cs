namespace Week1Assignment1.Models
{
    public class MyUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
