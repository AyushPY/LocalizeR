using LocalizeR.Core.DTO;
using LocalizeR.Core.Enums;
using LocalizeR.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalizeR.WebAPI.Controllers
{
    [AllowAnonymous]

    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<ApplicationUser>> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            ApplicationUser user = new ApplicationUser()
            {


                Email = registerUserDTO.Email,
                UserName = registerUserDTO.UserName,
                Location = registerUserDTO.Location,
                PhoneNumber = registerUserDTO.ContactNo,




            };
            IdentityResult result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(UserType.User.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserType.User.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }
                await _userManager.AddToRoleAsync(user, UserType.User.ToString());
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join("|", result.Errors.Select(error => error.Description));
                return Problem(errorMessage);
            }

        }
        [HttpPost("RegisterServiceProvider")]
        public async Task<ActionResult<ApplicationUser>> RegisterServiceProvider(RegisterServicerProviderDTO registerServicerProviderDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            Guid guid = Guid.NewGuid();
            if (registerServicerProviderDTO.ServiceType == "plumber")
            {
                registerServicerProviderDTO.ServiceId = 1;
            }
            else if (registerServicerProviderDTO.ServiceType == "electrician")
            {
                registerServicerProviderDTO.ServiceId = 2;
            }
            else if (registerServicerProviderDTO.ServiceType == "hvac")
            {
                registerServicerProviderDTO.ServiceId = 3;
            }
            else if (registerServicerProviderDTO.ServiceType == "cleaning")
            {
                registerServicerProviderDTO.ServiceId = 4;
            }
            else if (registerServicerProviderDTO.ServiceType == "furniture")
            {
                registerServicerProviderDTO.ServiceId = 5;
            }
            else if (registerServicerProviderDTO.ServiceType == "moving")
            {
                registerServicerProviderDTO.ServiceId = 6;
            }
            else if (registerServicerProviderDTO.ServiceType == "contracts")
            {
                registerServicerProviderDTO.ServiceId = 7;
            }
            ApplicationUser serviceprovider = new ApplicationUser()
            {
                UserName = registerServicerProviderDTO.UserName,
                Email = registerServicerProviderDTO.Email,
                PhoneNumber = registerServicerProviderDTO.ContactNo,
                Location = registerServicerProviderDTO.Location,
                BusinessName = registerServicerProviderDTO.BusinessName,
                ImageId = null,
                ImageData = null,
                ServiceType = registerServicerProviderDTO.ServiceType,
                ServiceId = registerServicerProviderDTO.ServiceId

            };
            IdentityResult result = await _userManager.CreateAsync(serviceprovider, registerServicerProviderDTO.Password);
            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(UserType.ServiceProvider.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserType.ServiceProvider.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }
                await _userManager.AddToRoleAsync(serviceprovider, UserType.ServiceProvider.ToString());
                await _signInManager.SignInAsync(serviceprovider, isPersistent: false);
                return Ok(serviceprovider);
            }
            else
            {
                string errorMessage = string.Join("|", result.Errors.Select(error => error.Description));
                return Problem(errorMessage);
            }
        }
        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }



            ApplicationUser? user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user == null)
            {
                return NoContent();
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok(new { username = user.UserName, email = user.Email });
                }
                else
                {
                    return Problem("Invalid Email Or Password");
                }

            }


        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
