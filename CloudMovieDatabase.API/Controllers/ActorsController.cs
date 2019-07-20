using System;
using System.Threading.Tasks;
using CloudMovieDatabase.API.Data;
using CloudMovieDatabase.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudMovieDatabase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ICloudRepository _repo;

        public ActorsController(ICloudRepository repo)
        {
            _repo = repo;
        }

        // GET api/actors
        [HttpGet]
        public async Task<IActionResult> GetActors() 
        {
           var actors = await _repo.GetActors();

            return Ok(actors);
        }

        // GET api/actors/3
        [HttpGet("{id}", Name = "GetActor")]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await _repo.GetActor(id);
        
            return Ok(actor);
        }

        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetActorMovies(int id)
        {
            var actorMovies = await _repo.GetMoviesByActor(id);

            return Ok(actorMovies);
        }

        [HttpPost]
        public async Task<IActionResult> PostActor(Actor actor)
        {
            _repo.Add<Actor>(actor);

            if (await _repo.SaveAll())
                return CreatedAtRoute("GetActor", new { id = actor.Id}, actor);
            
            return BadRequest("Failed to add actor");

        } 

        [HttpPut("{id}/actor-movie/{movieId}")]
        public async Task<IActionResult> MovieToActor(int id, int movieId)
        {
            var actor = await _repo.GetActor(id);
            var movie = await _repo.GetMovie(movieId);

            var actorMovie = await _repo.GetActorMovieRelation(id, movieId);

            if (actorMovie != null)
                return BadRequest("This actor is already in that movie");

            if (await _repo.GetMovie(movieId) == null)
                return NotFound();

            if (movie.Year < actor.BirthDay.Year)
                return BadRequest("The actor was born later than the movie was made");

             actorMovie = new ActorMovie
             {
                 ActorId = id,
                 MovieId = movieId
             };

             _repo.Add<ActorMovie>(actorMovie);

             if (await _repo.SaveAll())
                return Ok();
            
            return BadRequest("Failed to add actor to movie");
        }
    }
}