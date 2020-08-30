using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace writeAJoke.Data 
{
    public class writeAJokeContext : IdentityDbContext
    {
        public writeAJokeContext (
            DbContextOptions<writeAJokeContext> options) 
            : base(options)
        {
        }

        public DbSet<writeAJoke.Models.Joke> Joke { get; set; }
    }
}