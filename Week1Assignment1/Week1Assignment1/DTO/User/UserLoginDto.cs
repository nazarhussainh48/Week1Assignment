using System.ComponentModel.DataAnnotations;

namespace Week1Assignment1.DTO.User
{
    public class UserLoginDto
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Password { get; set; }
    }
}
