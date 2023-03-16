using System.ComponentModel.DataAnnotations;

namespace Week1Assignment1.Models
{
    public class ResetPasswordModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
