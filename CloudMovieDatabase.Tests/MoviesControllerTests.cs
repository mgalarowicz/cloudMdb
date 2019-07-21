using System;
using AutoMapper;
using CloudMovieDatabase.API.Controllers;
using CloudMovieDatabase.API.Data;
using CloudMovieDatabase.API.Dtos;
using CloudMovieDatabase.API.Helpers;
using CloudMovieDatabase.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 1)]
namespace CloudMovieDatabase.Tests
{
    public class MoviesControllerTests
    {
        private Movie[] movieList;
        private MovieForUpdateDto movieDto;

        public MoviesControllerTests()
        {
            movieList = new Movie[]
            {
                new Movie { Title = "Anything", Genre = Enums.GenreOfMovie.Crime, Year = 2000 },
                new Movie { Title = "Anything2", Genre = Enums.GenreOfMovie.Drama, Year = 2001 },
                new Movie { Title = "Anything3", Genre = Enums.GenreOfMovie.Comedy, Year = 2002 }
            };

            movieDto = new MovieForUpdateDto { Title = "Something", Genre = Enums.GenreOfMovie.Family, Year = 1999};
        }

        [Fact]
        public async void GetMovies_Test()
        {        
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetMovies()).ReturnsAsync(movieList);
            var controller = new MoviesController(repository.Object, mapper.Object);
            
            // Act
            var result = await controller.GetMovies();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Movie[]>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(movieList.Length, ((Movie[])okResult.Value).Length);
        }

        [Fact]
        public async void GetMovie_Test()
        {        
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetMovie(It.IsAny<int>())).ReturnsAsync(movieList[0]);
            var controller = new MoviesController(repository.Object, mapper.Object);
            
            // Act
            var result = await controller.GetMovie(It.IsAny<int>());
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Movie>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async void GetMoviesByActor_Test()
        {
           // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetMoviesBySingleActor(It.IsAny<int>())).ReturnsAsync(movieList);
            var controller = new MoviesController(repository.Object, mapper.Object);
            
            // Act
            var result = await controller.GetMoviesByActor(It.IsAny<int>());
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Movie[]>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(movieList.Length, ((Movie[])okResult.Value).Length);
        }

        [Fact]
        public async void GetMoviesByYear_Test()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetMoviesByProductionYear(It.IsAny<int>())).ReturnsAsync(movieList);
            var controller = new MoviesController(repository.Object, mapper.Object);
            
            // Act
            var result = await controller.GetMoviesByYear(It.IsAny<int>());
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Movie[]>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(movieList.Length, ((Movie[])okResult.Value).Length);
        }

        [Fact]
        public async void PostMovie_Test()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.Add(It.IsAny<Movie>()));
            var controller = new MoviesController(repository.Object, mapper.Object);

            //Act
            await controller.PostMovie(It.IsAny<Movie>());

            //Assert
            repository.Verify(x => x.Add(It.IsAny<Movie>()));
        }

        [Fact]
        public async void UpdateMovie_Test()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetMovie(It.IsAny<int>())).ReturnsAsync(movieList[0]);
            mapper.Setup(x => x.Map<MovieForUpdateDto>(It.IsAny<Movie>())).Returns(movieDto);
            var controller = new MoviesController(repository.Object, mapper.Object);

            //Act
            var result = await controller.UpdateMovie(It.IsAny<int>(), It.IsAny<MovieForUpdateDto>());

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<MovieForUpdateDto>(objectResult.Value);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact]
        public async void DeleteMovie_Test()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.Delete(It.IsAny<Movie>()));
            var controller = new MoviesController(repository.Object, mapper.Object);

            //Act
            await controller.DeleteMovie(It.IsAny<int>());

            //Assert
            repository.Verify(x => x.Delete(It.IsAny<Movie>()));
        }
    }
}
