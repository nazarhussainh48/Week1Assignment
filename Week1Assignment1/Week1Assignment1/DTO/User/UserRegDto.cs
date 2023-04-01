using System.ComponentModel.DataAnnotations;

namespace Week1Assignment1.DTO.User
{
    public class UserRegDto
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Password { get; set; }
    }
}
