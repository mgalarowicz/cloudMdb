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
            var actorMovies = await _repo.GetMoviesBySingleActor(id);

            return Ok(actorMovies);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetActorsFromMovie(int movieId)
        {
            var movieActors =  await _repo.GetMovieActors(movieId);

            return Ok(movieActors);
        }

        [HttpPost]
        public async Task<IActionResult> PostActor(Actor actor)
        {
            _repo.Add<Actor>(actor);

            if (await _repo.SaveAll())
                return CreatedAtRoute("GetActor", new { id = actor.Id}, actor);
            
            return BadRequest("Failed to add actor");

        } 
    }
}