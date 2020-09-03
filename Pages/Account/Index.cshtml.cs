using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using writeAJoke.Models;

namespace writeAJoke.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly writeAJoke.Data.writeAJokeContext _context;

        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            writeAJoke.Data.writeAJokeContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string UserName {get; set;}


        [TempData]
        public string StatusMessage {get; set;}

        [BindProperty]
        public InputModel Input {get; set;}

        public IList<Joke> JokeList { get;set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var userEmail = await _userManager.GetEmailAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);

            UserName = userName;

            JokeList = getJokesFromDB(userId);

            Input = new InputModel
            {
                Email = userEmail,
                Name = userName
            };

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
                UpdateUserData(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage(); 
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            var JokeToDelete = await _context.Joke.FindAsync(id);

            if(JokeToDelete != null)
            {
                _context.Joke.Remove(JokeToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        private List<Joke> getJokesFromDB(string userId)
        {
            var Jokes = from j in _context.Joke
                        select j;

            Jokes = Jokes.Where(g => g.UserId == userId);
            
            var JokesList = Jokes.ToList();
            return JokesList;
        }

        private async void UpdateUserData(IdentityUser user)
        {
            var setUserNameResult = await _userManager.SetUserNameAsync(user,Input.Name);
            var setEmailResult = await _userManager.SetEmailAsync(user , Input.Email);

            if(!setUserNameResult.Succeeded || !setEmailResult.Succeeded)
            {
                UpdateFailHandler(user);
            }
        }

        private async void UpdateFailHandler(IdentityUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            throw new InvalidOperationException($"unexpected Error occured while setting email for ID '{userId}'.");
        }
    }
}