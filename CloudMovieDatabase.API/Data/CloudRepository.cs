using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<IEnumerable<Actor>> GetActors()
        {
            var actors = await _context.Actors.Include(p => p.Filmography).ToListAsync();

            return actors;
        }

        public async Task<Actor> GetActor(int id)
        {
            var actor = await _context.Actors.Include(p => p.Filmography).FirstOrDefaultAsync(x => x.Id == id);

            return actor;
        }

        public async Task<IEnumerable<Actor>> GetMovieActors(int movieId) 
        {
            var movieActors = await _context.ActorMovie.Where(ma => ma.MovieId == movieId)
                .Select(a => a.Actor).Include(a => a.Filmography).ToListAsync(); 
        
            return movieActors;
        }

        
    
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = await _context.Movies.Include(p => p.StarringActors).ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _context.Movies.Include(p => p.StarringActors).FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByActor(int actorId)
        {
            var moviesByActor = await _context.ActorMovie.Where(ma => ma.ActorId == actorId)
                    .Select(a => a.Movie).ToListAsync();

            return moviesByActor;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByProductionYear(int year)
        {
            var moviesByProductionYear = await _context.Movies.Where(m => m.Year == year).ToListAsync();

            return moviesByProductionYear;
        }



        public async Task<ActorMovie> GetActorMovieRelation(int actorId, int movieId)
        {
            return await _context.ActorMovie.FirstOrDefaultAsync(am => 
                am.ActorId == actorId && am.MovieId == movieId);
        }
    }
}