using System.Collections.Generic;
using System.Linq;
using CloudMovieDatabase.API.Models;
using Newtonsoft.Json;

namespace CloudMovieDatabase.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedActors()
        {
            if (!_context.Actors.Any())
            {
                var actorData = System.IO.File.ReadAllText("Data/ActorSeedData.json");
                var actors = JsonConvert.DeserializeObject<List<Actor>>(actorData);
                foreach (var actor in actors)
                {
                    _context.Actors.Add(actor);
                }

                _context.SaveChanges();
            }
        }

        public void SeedMovies()
        {
            if (!_context.Movies.Any())
            {
                var movieData = System.IO.File.ReadAllText("Data/MovieSeedData.json");
                var movies = JsonConvert.DeserializeObject<List<Movie>>(movieData);
                foreach (var movie in movies)
                {
                    _context.Movies.Add(movie);
                }

                _context.SaveChanges();
            }    
        }
    }
}