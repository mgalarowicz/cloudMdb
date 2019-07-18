using System.Threading.Tasks;
using CloudMovieDatabase.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudMovieDatabase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICloudRepository _repo;

        public MoviesController(ICloudRepository repo)
        {
            _repo = repo;
        }

        // GET api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies() 
        {
            var movies = await _repo.GetMovies();

            return Ok(movies);
        }

        // GET api/movies/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _repo.GetMovie(id);
        
            return Ok(movie);
        }
    }
}