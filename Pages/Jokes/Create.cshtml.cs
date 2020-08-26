using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using writeAJoke.Data;
using writeAJoke.Models;
using System.Security.Claims;

namespace writeAJoke.Pages.Jokes
{
    public class CreateModel : PageModel
    {
        private readonly writeAJoke.Data.writeAJokeContext _context;

        public CreateModel(writeAJoke.Data.writeAJokeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Joke Joke { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Joke.UserId = userId;

            _context.Joke.Add(Joke);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
