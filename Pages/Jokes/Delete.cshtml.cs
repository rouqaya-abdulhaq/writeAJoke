using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using writeAJoke.Data;
using writeAJoke.Models;

namespace writeAJoke.Pages.Jokes
{
    public class DeleteModel : PageModel
    {
        private readonly writeAJoke.Data.writeAJokeContext _context;

        public DeleteModel(writeAJoke.Data.writeAJokeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Joke Joke { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Joke = await _context.Joke.FirstOrDefaultAsync(m => m.ID == id);

            if (Joke == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Joke = await _context.Joke.FindAsync(id);

            if (Joke != null)
            {
                _context.Joke.Remove(Joke);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
