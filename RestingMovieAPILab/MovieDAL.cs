using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace RestingMovieAPILab
{
    public class MovieDAL
    {
        //CREATE
        public void CreateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"insert into movie values(0, \"{m.Title}\", '{m.Category}')";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }

        //READ
        //read one movie by id
        public Movie GetMovie(int id)
        {
          
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movie where id={id}";
                connect.Open();
                Movie m = connect.Query<Movie>(sql).FirstOrDefault();
                connect.Close();

                return m;
            }

        }

        //read by category
        public List<Movie> GetByCategory(string value)
        {            
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movie where category='{value}'";
                connect.Open();
                List<Movie> m = connect.Query<Movie>(sql).ToList();
                connect.Close();

                return m;
            }

        }

        public Movie GetRandomByCategory(string category)
        {            
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movie where category='{category}'";
                connect.Open();
                List<Movie> m = connect.Query<Movie>(sql).ToList();
                connect.Close();

                Random r = new Random();
                int random = r.Next(0, m.Count());
                return m[random];
            }
        }

        //read all movies from DB
        public List<Movie> GetMovies()
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movie";
                connect.Open();
                List<Movie> m = connect.Query<Movie>(sql).ToList();
                connect.Close();

                return m;
            }
        }

        //UPDATE
        public void UpdateMovie(int id, Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"update movie set title=\"{m.Title}\", genre='{m.Category}' where id={id}";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();

            }

        }

        //DELETE
        public void DeleteMovie(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"delete from movie where id={id}";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();

            }

        }
    }
}
