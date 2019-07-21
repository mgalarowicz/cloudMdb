using CloudMovieDatabase.API.Helpers;

namespace CloudMovieDatabase.API.Dtos
{
    public class MovieForUpdateDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Enums.GenreOfMovie Genre { get; set; }
    }
}