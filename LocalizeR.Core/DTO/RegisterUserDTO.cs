using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LocalizeR.Core.DTO
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Pattern")]
        [Required(ErrorMessage = "Email Cannot be Empty")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is alredy in use")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Invalid Contact Number")]
        [Remote(action: "IsPhoneRegistered", controller: "Account", ErrorMessage = "Number is alredy in use")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter digits only")]
        public string? ContactNo { get; set; }
    }
}
