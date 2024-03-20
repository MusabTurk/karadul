using System.ComponentModel.DataAnnotations;

namespace Karadul.WebMVC.Dtos.LoginDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email Alanı zorunludur.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Alanı zorunludur.")]
        public string Password { get; set; }
    }
}
