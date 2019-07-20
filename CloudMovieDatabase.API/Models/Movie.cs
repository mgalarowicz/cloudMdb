using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudMovieDatabase.API.Models
{
    public class Movie
    {
        public enum GenreOfMovie
        {
            Drama = 1,
            Comedy,
            Thriller,
            Romance,
            Action,
            Horror,
            Crime,
            Adventure,
            Mystery,
            Family,
            Fantasy,
            SciFi,
            Music,
            Animation,
            Biography,
            History,
            Musical,
            War,
            Sport,
            Western
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public GenreOfMovie Genre { get; set; }
        public ICollection<ActorMovie> StarringActors { get; set; }
    }
}