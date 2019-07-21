using AutoMapper;
using CloudMovieDatabase.API.Dtos;
using CloudMovieDatabase.API.Models;

namespace CloudMovieDatabase.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovieForUpdateDto, Movie>();
        }
    }
}