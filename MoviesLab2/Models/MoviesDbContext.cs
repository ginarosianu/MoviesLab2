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
    }
}
