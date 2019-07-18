using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMovieDatabase.API.Models;

namespace CloudMovieDatabase.API.Data
{
    public interface ICloudRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<Actor>> GetActors();
         Task<Actor> GetActor(int id);
         Task<IEnumerable<Movie>> GetMovies();        
         Task<Movie> GetMovie(int id);
         
    }
}