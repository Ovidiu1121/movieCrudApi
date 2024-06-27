using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Controllers;
using MovieCrudApi.Movies.Controllers.interfaces;
using MovieCrudApi.Movies.Service.interfaces;
using MovieCrudApi.System.Constant;
using MovieCrudApi.System.Exceptions;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    Mock<IMovieCommandService> _command;
    Mock<IMovieQueryService> _query;
    MovieApiController _controller;

    public TestController()
    {
        _command = new Mock<IMovieCommandService>();
        _query = new Mock<IMovieQueryService>();
        _controller = new MovieController(_command.Object, _query.Object);
    }

    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {

        _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemDoesNotExist(Constants.MOVIE_DOES_NOT_EXIST));
           
        var result = await _controller.GetAll();

        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(404, notFound.StatusCode);
        Assert.Equal(Constants.MOVIE_DOES_NOT_EXIST, notFound.Value);

    }

    [Fact]
    public async Task GetAll_ValidData()
    {

        var movies = TestMovieFactory.CreateMovies(5);

        _query.Setup(repo => repo.GetAll()).ReturnsAsync(movies);

        var result = await _controller.GetAll();

        var okresult = Assert.IsType<OkObjectResult>(result.Result);

        var moviesAll = Assert.IsType<ListMovieDto>(okresult.Value);

        Assert.Equal(5, moviesAll.movieList.Count);
        Assert.Equal(200, okresult.StatusCode);
        
    }
    
    [Fact]
    public async Task Create_InvalidData()
    {

        var create = new CreateMovieRequest()
        {
            Title="Test",
            Duration=120,
            Genre="test"
        };

        _command.Setup(repo => repo.CreateMovie(It.IsAny<CreateMovieRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.MOVIE_ALREADY_EXIST));

        var result = await _controller.CreateMovie(create);

        var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal(400,bad.StatusCode);
        Assert.Equal(Constants.MOVIE_ALREADY_EXIST, bad.Value);

    }

    [Fact]
    public async Task Create_ValidData()
    {

        var create = new CreateMovieRequest()
        {
            Title="Test",
            Duration=120,
            Genre="test"
        };

        var movie = TestMovieFactory.CreateMovie(5);

        movie.Title=create.Title;
        movie.Duration=create.Duration;
        movie.Genre=create.Genre;

        _command.Setup(repo => repo.CreateMovie(create)).ReturnsAsync(movie);

        var result = await _controller.CreateMovie(create);

        var okResult= Assert.IsType<CreatedResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 201);
        Assert.Equal(movie, okResult.Value);

    }
    
    [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdateMovieRequest()
            {
                Title="Test",
                Duration=120,
                Genre="test"
            };

            _command.Setup(repo => repo.UpdateMovie(3, update)).ThrowsAsync(new ItemDoesNotExist(Constants.MOVIE_DOES_NOT_EXIST));

            var result = await _controller.UpdateMovie(3, update);

            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.MOVIE_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Update_ValidData()
        {

            var update = new UpdateMovieRequest()
            {
                Title="Test",
                Duration=120,
                Genre="test"
            };

            var movie = TestMovieFactory.CreateMovie(5);
            movie.Title=update.Title;
            movie.Duration=update.Duration.Value;
            movie.Genre=update.Genre;

            _command.Setup(repo=>repo.UpdateMovie(5,update)).ReturnsAsync(movie);

            var result = await _controller.UpdateMovie(5, update);

            var okResult=Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, movie);

        }


        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo=>repo.DeleteMovie(2)).ThrowsAsync(new ItemDoesNotExist(Constants.MOVIE_DOES_NOT_EXIST));

            var result= await _controller.DeleteMovie(2);

            var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.MOVIE_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var movie = TestMovieFactory.CreateMovie(1);

            _command.Setup(repo => repo.DeleteMovie(1)).ReturnsAsync(movie);

            var result = await _controller.DeleteMovie(1);

            var okResult=Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(movie, okResult.Value);

        }
    
}