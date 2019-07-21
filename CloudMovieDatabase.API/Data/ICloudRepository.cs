using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMovieDatabase.API.Models;

namespace CloudMovieDatabase.API.Data
{
    public interface ICloudRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

        //  void Update<T>(T entity) where T: class;

         Task<bool> SaveAll();


         Task<IEnumerable<Actor>> GetActors();

         Task<Actor> GetActor(int id);

         Task<IEnumerable<Actor>> GetMovieActors(int movieId);



         Task<IEnumerable<Movie>> GetMovies();   

         Task<Movie> GetMovie(int id);

         Task<IEnumerable<Movie>> GetMoviesBySingleActor(int actorId);

         Task<IEnumerable<Movie>> GetMoviesByProductionYear(int year);



         Task<ActorMovie> GetActorMovieRelation(int actorId, int movieId);
    }
}