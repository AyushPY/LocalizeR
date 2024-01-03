using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LocalizeR.Core.DTO
{
    public class RegisterServicerProviderDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Pattern")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is alredy in use")]

        public string? Email { get; set; }
        [Required(ErrorMessage = "Business Name is required")]
        public string? BusinessName { get; set; }
        [Required(ErrorMessage = "Invalid Contact Number")]
        [Remote(action: "IsPhoneRegistered", controller: "Account", ErrorMessage = "Number is alredy in use")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter digits only")]

        public string? ContactNo { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }
        public string? ServiceType { get; set; }
        [Required(ErrorMessage = "Password Cannot Be Blank")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
        public byte[]? ImageData { get; set; }

        public int? ServiceId { get; set; }


    }
}
