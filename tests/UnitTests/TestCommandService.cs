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

public class TestCommandService
{
    Mock<IMovieRepository> _mock;
    IMovieCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<IMovieRepository>();
        _service = new MovieCommandService(_mock.Object);
    }
    
    [Fact]
    public async Task Create_InvalidData()
    {
        var create = new CreateMovieRequest()
        {
            Title="Test",
            Duration=0,
            Genre="test"
        };

        var movie = TestMovieFactory.CreateMovie(5);

        _mock.Setup(repo => repo.GetByTitleAsync("Test")).ReturnsAsync(movie);
                
        var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateMovie(create));

        Assert.Equal(Constants.MOVIE_ALREADY_EXIST, exception.Message);



    }

    [Fact]
    public async Task Create_ReturnMovie()
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

        _mock.Setup(repo => repo.CreateMovie(It.IsAny<CreateMovieRequest>())).ReturnsAsync(movie);

        var result = await _service.CreateMovie(create);

        Assert.NotNull(result);
        Assert.Equal(result, movie);
    }
    
    [Fact]
    public async Task Update_ItemDoesNotExist()
    {
        var update = new UpdateMovieRequest()
        {
            Title="Test",
            Duration=120,
            Genre="test"
        };

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((MovieDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateMovie(1, update));

        Assert.Equal(Constants.MOVIE_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task Update_InvalidData()
    {
        var update = new UpdateMovieRequest()
        {
            Title="Test",
            Duration=120,
            Genre="test"
        };

        _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((MovieDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateMovie(5, update));

        Assert.Equal(Constants.MOVIE_DOES_NOT_EXIST, exception.Message);

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

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(movie);
        _mock.Setup(repoo => repoo.UpdateMovie(It.IsAny<int>(), It.IsAny<UpdateMovieRequest>())).ReturnsAsync(movie);

        var result = await _service.UpdateMovie(5, update);

        Assert.NotNull(result);
        Assert.Equal(movie, result);

    }

    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.DeleteMovieById(It.IsAny<int>())).ReturnsAsync((MovieDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteMovie(5));

        Assert.Equal(exception.Message, Constants.MOVIE_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var movie = TestMovieFactory.CreateMovie(5);

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(movie);

        var result= await _service.DeleteMovie(1);

        Assert.NotNull(result);
        Assert.Equal(movie, result);


    }
    
    
}