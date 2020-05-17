using Microsoft.EntityFrameworkCore;
using MoviesLab2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesLab2.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> 
            options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }

   /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>().HasData(
            new Movie
            {
                Id = 1,
                Title = "First Task",
                Description = "this is the description of the task",
                Genre =Genre.Action ,
                Minute = 100, 
                YearOfRelease = 2000, 
                Director = "cineva",
                DateAdded = DateTime.Now,
                Rating = 2,
                Watched = true
            });
    }*/


}
