using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using writeAJoke.Models;


namespace writeAJoke.Pages.User
{
    public class RegisterModel : PageModel 
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _Logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input {get; set;}
        public string ReturnUrl {get; set;}

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        } 

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = Input.Name,
                    Email = Input.Email,
                };

                var result = await _userManager.CreateAsync(user,Input.Password);
                if(result.Succeeded)
                {
                    _Logger.LogInformation("new user created");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/ConfirmEmail",
                        pageHandler : null,
                        values :  new {userId = user.Id, code = code},
                        protocol : Request.Scheme
                    );

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your Email", 
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user , isPersistent: false);

                    if(String.IsNullOrWhiteSpace(returnUrl))
                    {
                        return LocalRedirect("~/");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
