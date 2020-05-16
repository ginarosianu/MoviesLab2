using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesLab2.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesDbContext(serviceProvider.GetRequiredService<DbContextOptions<MoviesDbContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB table has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        Description = "sirop",
                        Genre = Genre.Comedy,
                        Minute = 100,
                        YearOfRelease = 2019 - 10 - 03,
                        Director = "Ion",
                        DateAdded = DateTime.Parse("1990-12-5"),
                        Rating = 2,
                        Watched = true
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        Description = "pistolari",
                        Genre = Genre.Action,
                        Minute = 150,
                        YearOfRelease = 2019 - 11 - 03,
                        Director = "Gheorghe",
                        DateAdded = DateTime.Parse("2000-10-10-12-30-00"),
                        //DateAdded = DateTime(2019,03,10,12,30,00),
                        Rating = 1,
                        Watched = false
                    }

                ); ;
                context.SaveChanges();
            }
        }
    }

}


