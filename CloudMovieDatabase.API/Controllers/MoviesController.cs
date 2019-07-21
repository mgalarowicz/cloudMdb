using System;
using System.Threading.Tasks;
using AutoMapper;
using CloudMovieDatabase.API.Data;
using CloudMovieDatabase.API.Dtos;
using CloudMovieDatabase.API.Models;
using CloudMovieDatabase.API.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudMovieDatabase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICloudRepository _repo;
        private readonly IMapper _mapper;

        public MoviesController(ICloudRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies() 
        {
            var movies = await _repo.GetMovies();

            return Ok(movies);
        }

        // GET api/movies/3
        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _repo.GetMovie(id);
        
            return Ok(movie);
        }

        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetMoviesByYear(int year)
        {
            var getByYear = await _repo.GetMoviesByProductionYear(year);

            return Ok(getByYear);
        }

        [HttpGet("actor/{actorId}")]
        public async Task<IActionResult> GetMoviesByActor(int actorId)
        {
            var getByActor = await _repo.GetMoviesBySingleActor(actorId);

            return Ok(getByActor);
        }

        [HttpPost]
        [MovieValidation]
        public async Task<IActionResult> PostMovie(Movie movie)
        {
            _repo.Add(movie);

            if (await _repo.SaveAll())
                return CreatedAtRoute("GetMovie", new { id = movie.Id}, movie);
            
            return BadRequest("Failed to add a movie");
        }

        // [HttpPut]
        // [MovieValidation]
        // public async Task<IActionResult> UpdateMovie(Movie movie)
        // {
        //     _repo.Update(movie);

        //     if (await _repo.SaveAll())
        //         return CreatedAtRoute("GetMovie", new { id = movie.Id}, movie);
            
        //     throw new Exception($"Updating movie {movie.Title} failed on save");
        // }

        [HttpPut("{id}")]
        [MovieValidation]
        public async Task<IActionResult> UpdateMovie(int id, MovieForUpdateDto movieForUpdateDto)
        {
            var movie = await _repo.GetMovie(id);

            _mapper.Map(movieForUpdateDto, movie);

            if (await _repo.SaveAll())
                return CreatedAtRoute("GetMovie", new { id = movie.Id}, movie);
            
           throw new Exception($"Updating movie {movie.Title} failed on save");
        }

        [HttpPut("{id}/movie-actor/{actorId}")]
        public async Task<IActionResult> ActorToMovie(int id, int actorId)
        {
            var actor = await _repo.GetActor(actorId);
            var movie = await _repo.GetMovie(id);

            var actorMovie = await _repo.GetActorMovieRelation(actorId, id);

            if (actorMovie != null)
                return BadRequest("This actor is already in that movie");

            if (actor == null || movie == null)
                return NotFound();

            if (movie.Year < actor.BirthDay.Year)
                return BadRequest("The actor was born later than the movie was made");
  
             actorMovie = new ActorMovie
             {
                 ActorId = actorId,
                 MovieId = id
             };

             _repo.Add(actorMovie);

             if (await _repo.SaveAll())
                return Ok();
            
            return BadRequest("Failed to add actor to movie");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _repo.GetMovie(id);

            _repo.Delete(movie);

            if (await _repo.SaveAll())
                return Ok();
            
            return BadRequest("Failed to delete movie");
        }
    }
}