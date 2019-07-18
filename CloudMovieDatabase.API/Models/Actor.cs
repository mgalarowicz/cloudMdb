using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudMovieDatabase.API.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        public DateTime BirthDay { get; set; }

        public ICollection<ActorMovie> Filmography { get; set; }
    }
}