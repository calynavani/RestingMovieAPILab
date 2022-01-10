using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestingMovieAPILab.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        MovieDAL mDB = new MovieDAL();

       
        [HttpGet]
        public List<Movie> GetMovies()
        {
            return mDB.GetMovies();
        }

       

        [HttpGet("{id}")]
        public Movie GetMovie(int id)
        {
            return mDB.GetMovie(id);
        }

       
        [HttpGet("category={value}")]
        public List<Movie> GetByCategory(string value)
        {
            List<Movie> m = mDB.GetByCategory(value);
            if (m.Count == 0)
            {
                m.Add(new Movie($"No Movies Found with Category = {value}"));
            }
            return m;
        }

        [HttpPost("create")]
        public string CreateMovie(Movie m)
        {
            mDB.CreateMovie(m);

            return $"{m.Title} successfully added";
        }


        [HttpDelete("delete/{id}")]
        public string DeleteMovie(int id)
        {
            mDB.DeleteMovie(id);

            return $"Movie {id} successfully deleted";
        }


        [HttpPut("update/{id}")]
        public string UpdateMovie(int id, Movie updatedMovie)
        {
            //We want our user to be able to select which properties
            //they wish to change and leave the rest alone. This will
            //make it so that for properties we wish to leave alone,
            //we don't have to re-enter their values. So will have to
            //compare what values have changed.

            Movie oldMovie = mDB.GetMovie(id);

            //Check updated movie for changed properties

            if (updatedMovie.Title == null)
            {
                updatedMovie.Title = oldMovie.Title;
            }
            if (updatedMovie.Category == null)
            {
                updatedMovie.Category = oldMovie.Category;
            }
            if (updatedMovie.Runtime == 0)
            {
                updatedMovie.Runtime = oldMovie.Runtime;
            }
            if (updatedMovie.Rating == null)
            {
                updatedMovie.Rating = oldMovie.Rating;
            }

            mDB.UpdateMovie(id, updatedMovie);

            return $"{updatedMovie.Title} id: {id} was successfully updated";
        }

        [HttpGet("random/{numMovies}")]
        public List<Movie> GetRandom(int numMovies)
        {
            List<Movie> m = mDB.GetMovies();
            List<Movie> randomMovies = new List<Movie>();
            List<int> indexs = new List<int>();
            Random r = new Random();

            if (numMovies > m.Count())
            {
                numMovies = m.Count();
            }

            for (int i = 1; i <= numMovies; i++)
            {
                int random = r.Next(0, m.Count());
                while (indexs.Contains(random))
                {
                    random = r.Next(0, m.Count());
                }
                indexs.Add(random);
            }

            foreach (int i in indexs)
            {
                randomMovies.Add(m[i]);
            }

            return randomMovies;
        }

        [HttpGet("randombycategory/{category}")]
        public Movie GetRandomByCategory(string category)
        {
            Movie m = mDB.GetRandomByCategory(category);
            if (m == null)
            {
                m = new Movie($"No movies with Genre: {category}");
            }

            return m;
        }



       
    }
}

