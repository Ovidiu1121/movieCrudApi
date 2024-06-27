using System.Threading.Tasks;
using Moq;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Repository.interfaces;
using MovieCrudApi.Movies.Service;
using MovieCrudApi.Movies.Service.interfaces;
using MovieCrudApi.System.Constant;
using MovieCrudApi.System.Exceptions;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestQueryService
{
    Mock<IMovieRepository> _mock;
    IMovieQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<IMovieRepository>();
        _service=new MovieQueryService(_mock.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {
        _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListMovieDto());

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAll());

        Assert.Equal(exception.Message, Constants.NO_MOVIES_EXIST);       

    }

    [Fact]
    public async Task GetAll_ReturnAllMovie()
    {

        var movies = TestMovieFactory.CreateMovies(5);

        _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(movies);

        var result = await _service.GetAll();

        Assert.NotNull(result);
        Assert.Contains(movies.movieList[1], result.movieList);

    }

    [Fact]
    public async Task GetById_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((MovieDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

        Assert.Equal(Constants.MOVIE_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetById_ReturnMovie()
    {

        var movie = TestMovieFactory.CreateMovie(5);

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(movie);

        var result = await _service.GetById(5);

        Assert.NotNull(result);
        Assert.Equal(movie, result);

    }

    [Fact]
    public async Task GetByTitle_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByTitleAsync("")).ReturnsAsync((MovieDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByTitle(""));

        Assert.Equal(Constants.MOVIE_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetByTitle_ReturnMovie()
    {

        var movie = TestMovieFactory.CreateMovie(1);

        movie.Title="test";

        _mock.Setup(repo => repo.GetByTitleAsync("test")).ReturnsAsync(movie);

        var result = await _service.GetByTitle("test");

        Assert.NotNull(result);
        Assert.Equal(movie, result);

    }
    
    
}