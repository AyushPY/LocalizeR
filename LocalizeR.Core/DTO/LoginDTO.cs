using System.ComponentModel.DataAnnotations;

namespace LocalizeR.Core.DTO
{
    public class LoginDTO
    {
        //[Required(ErrorMessage = "Email cannot be blank")]
        //[EmailAddress(ErrorMessage = "Email is not in correct format")]

        //public string? Email { get; set; } = string.Empty;

        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string? Password { get; set; }

    }
}
