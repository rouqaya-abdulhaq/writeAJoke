using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using writeAJoke.Models;

namespace writeAJoke.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _Logger;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _Logger = logger;
        }

        [BindProperty]
        public InputModel Input {get; set;}
        public string ReturnUrl {get; set;}
        [TempData]
        public string ErrorMessage {get; set;}

        public void OnGet(string returnUrl = null)
        {
            if(!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Name, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if(result.Succeeded)
                {
                    _Logger.LogInformation("User logged in.");
                    return Redirect(returnUrl);
                }
                if(result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa",new {ReturnUrl = returnUrl, RemeberMe = Input.RememberMe});
                }
                if(result.IsLockedOut)
                {
                    _Logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Invalid login attempt");
                    return Page();
                }
            }
            return Page();
        }
    }
}