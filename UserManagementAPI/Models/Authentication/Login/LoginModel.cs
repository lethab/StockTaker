using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.Authentication.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Password { get; set; }
    }
}
