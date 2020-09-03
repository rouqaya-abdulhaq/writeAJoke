using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using writeAJoke.Data;
using System;
using System.Linq;

namespace writeAJoke.Models
{
    public static class SeedData 
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new writeAJokeContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<writeAJokeContext>>()))
            {
                if(context.Joke.Any())
                {
                    return;
                }

                context.Joke.AddRange(
                    new Joke
                    {
                        Title = "vegan joke premttion",
                        Body = "can i tell you a vegan joke? i promise it won't be cheesy",
                        Genre = "dad jokes",
                        UserId = "",
                        UserName = "Write A Joke"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}