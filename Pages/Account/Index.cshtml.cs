using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using writeAJoke.Models;

namespace writeAJoke.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
                            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string UserName {get; set;}

        public bool IsEmailConfirmed {get; set;}

        [TempData]
        public string StatusMessage {get; set;}

        [BindProperty]
        public InputModel Input {get; set;}

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var userEmail = await _userManager.GetEmailAsync(user);

            UserName = userName;

            Input = new InputModel
            {
                Email = userEmail
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        } 

        public async Task<IActionResult> OnPostAsync ()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            var name = await _userManager.GetUserNameAsync(user);
            if(Input.Email != email || Input.Name != name)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user,Input.Name);
                var setEmailResult = await _userManager.SetEmailAsync(user , Input.Email);

                if(!setUserNameResult.Succeeded || !setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"unexpected Error occured while setting email for ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage(); 
        }
    }
}