using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesLab2.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public int Minute { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public DateTime DateAdded { get; set; }
        // [Range(1, 10)]
        public int Rating { get; set; }
        public bool Watched { get; set; }
        public List<Comment> Comments {get; set;}
    }
}
