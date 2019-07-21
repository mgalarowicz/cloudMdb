using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudMovieDatabase.API.Helpers;

namespace CloudMovieDatabase.API.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public Enums.GenreOfMovie Genre { get; set; }
        public ICollection<ActorMovie> StarringActors { get; set; }
    }
}