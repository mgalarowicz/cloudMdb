using System;
using CloudMovieDatabase.API.Controllers;
using CloudMovieDatabase.API.Data;
using CloudMovieDatabase.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudMovieDatabase.Tests
{
    public class ActorsControllerTests
    {
        private Actor[] actorList;

        public ActorsControllerTests()
        {
            actorList = new Actor[]
            {
                new Actor { Id=1, FirstName = "Anything", LastName = "Something", Gender = "male", BirthDay = DateTime.Now.AddYears(-10) },
                new Actor { Id=2, FirstName = "Anything2", LastName = "Something2", Gender = "male", BirthDay = DateTime.Now.AddYears(-20) },
                new Actor { Id=3, FirstName = "Anything3", LastName = "Something3", Gender = "male", BirthDay = DateTime.Now.AddYears(-30) },   
            };
        }

        [Fact]
        public async void GetActors_Test()
        {        
            // Arrange
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetActors()).ReturnsAsync(actorList);
            var controller = new ActorsController(repository.Object);
            
            // Act
            var result = await controller.GetActors();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Actor[]>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(actorList.Length, ((Actor[])okResult.Value).Length);
        }

        [Fact]
        public async void GetActor_Test()
        {        
            // Arrange
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetActor(It.IsInRange<int>(1,3, Range.Inclusive))).ReturnsAsync(actorList[0]);
            var controller = new ActorsController(repository.Object);
            
            // Act
            var result = await controller.GetActor(actorList[0].Id);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Actor>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async void GetActorsFromMovie_Test()
        {
            // Arrange
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.GetMovieActors(It.IsAny<int>())).ReturnsAsync(actorList);
            var controller = new ActorsController(repository.Object);
            
            // Act
            var result = await controller.GetActorsFromMovie(It.IsAny<int>());
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Actor[]>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(actorList.Length, ((Actor[])okResult.Value).Length);
        }

        [Fact]
        public async void PostActor_Test()
        {
            // Arrange
            var repository = new Mock<ICloudRepository>();
            repository.Setup(x => x.Add(It.IsAny<Actor>()));
            var controller = new ActorsController(repository.Object);

            //Act
            await controller.PostActor(It.IsAny<Actor>());

            //Assert
            repository.Verify(x => x.Add(It.IsAny<Actor>()));
        }
    }
}