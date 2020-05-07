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
    public class IndexModel : PageModel
    {
        private readonly writeAJoke.Data.writeAJokeContext _context;

        public IndexModel(writeAJoke.Data.writeAJokeContext context)
        {
            _context = context;
        }

        public IList<Joke> Joke { get;set; }

        public async Task OnGetAsync()
        {
            Joke = await _context.Joke.ToListAsync();
        }
    }
}
