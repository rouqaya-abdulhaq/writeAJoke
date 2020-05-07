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
    public class DetailsModel : PageModel
    {
        private readonly writeAJoke.Data.writeAJokeContext _context;

        public DetailsModel(writeAJoke.Data.writeAJokeContext context)
        {
            _context = context;
        }

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
    }
}
