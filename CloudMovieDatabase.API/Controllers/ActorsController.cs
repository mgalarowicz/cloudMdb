using System;
using System.Threading.Tasks;
using CloudMovieDatabase.API.Data;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await _repo.GetActor(id);
        
            return Ok(actor);
        }
    }
}