using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public SelectList Genres {get; set;}
        [BindProperty(SupportsGet = true)]
        public string JokeGenre {get; set;}

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from j in _context.Joke
                                            orderby j.Genre
                                            select j.Genre;

            var Jokes = from j in _context.Joke
                        select j;
                        
            if(!string.IsNullOrEmpty(SearchString))
            {
                Jokes = Jokes.Where(s => s.Title.Contains(SearchString));
            }
            if(!string.IsNullOrEmpty(JokeGenre))
            {
                Jokes = Jokes.Where(g => g.Genre == JokeGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Joke = await Jokes.ToListAsync();
        }

    }
}
