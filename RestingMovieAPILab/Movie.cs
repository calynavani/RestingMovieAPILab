using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestingMovieAPILab
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Runtime { get; set; }
        public string Rating { get; set; }

        public Movie() { }

        public Movie(string error)
        {
            Title = error;
        }
    }
}
