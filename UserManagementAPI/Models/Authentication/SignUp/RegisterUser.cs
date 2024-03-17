using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is Requerd")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is Requerd")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Requerd")]
        public string? Password { get; set; }
    }
}
