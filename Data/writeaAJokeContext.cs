using Microsoft.EntityFrameworkCore;

namespace writeAJoke.Data 
{
    public class writeAJokeContext : DbContext
    {
        public writeAJokeContext (
            DbContextOptions<writeAJokeContext> options) 
            : base(options)
        {
        }

        public DbSet<writeAJoke.Models.Joke> Joke { get; set; }
    }
}