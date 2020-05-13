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
        [BindProperty(SupportsGet = true)]
        public string SearchString {get; set;}

        public async Task OnGetAsync()
        {
            var Jokes = from j in _context.Joke
                        select j;
            if(!string.IsNullOrEmpty(SearchString))
            {
                Jokes = Jokes.Where(s => s.Title.Contains(SearchString));
            }
            Joke = await Jokes.ToListAsync();
        }
    }
}
