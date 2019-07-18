using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMovieDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudMovieDatabase.API.Data
{
    public class CloudRepository : ICloudRepository
    {
        private readonly DataContext _context;

        public CloudRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Actor> GetActor(int id)
        {
            var actor = await _context.Actors.Include(p => p.Filmography).FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            var actors = await _context.Actors.Include(p => p.Filmography).ToListAsync();

            return actors;
        }

        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _context.Movies.Include(p => p.StarringActors).FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = await _context.Movies.Include(p => p.StarringActors).ToListAsync();

            return movies;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}