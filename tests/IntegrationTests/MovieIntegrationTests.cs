using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;
using Newtonsoft.Json;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class MovieIntegrationTests:IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MovieIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Movie/create";
        var movie = new CreateMovieRequest() { Title = "new title", Duration = 90, Genre = "new genre" };
        var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Movie>(responseString);

        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
        Assert.Equal(movie.Duration, result.Duration);
        Assert.Equal(movie.Genre, result.Genre);
    }
    
    [Fact]
    public async Task Post_Create_MovieAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Movie/create";
        var movie = new CreateMovieRequest() { Title = "new title", Duration = 90, Genre = "new genre" };
        var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Movie/create";
        var movie = new CreateMovieRequest() { Title = "new title", Duration = 90, Genre = "new genre" };
        var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Movie>(responseString)!;

        request = "/api/v1/Movie/update/"+result.Id;
        var updateMovie = new UpdateMovieRequest() { Title = "updated title", Duration = 90, Genre = "updated genre" };
        content = new StringContent(JsonConvert.SerializeObject(updateMovie), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Movie>(responseString)!;

        Assert.Equal(updateMovie.Title, result.Title);
        Assert.Equal(updateMovie.Duration, result.Duration);
        Assert.Equal(updateMovie.Genre, result.Genre);
    }
    
    [Fact]
    public async Task Put_Update_MovieDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Movie/update/1";
        var updateMovie = new UpdateMovieRequest() { Title = "updated title", Duration = 90, Genre = "updated genre" };
        var content = new StringContent(JsonConvert.SerializeObject(updateMovie), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Delete_Delete_MovieExists_ReturnsDeletedMovie()
    {

        var request = "/api/v1/Movie/create";
        var movie = new CreateMovieRequest() { Title = "new title", Duration = 90, Genre = "new genre" };
        var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Movie>(responseString)!;

        request = "/api/v1/Movie/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Delete_MovieDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Movie/delete/66";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        
    }

    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Movie/create";
        var movie = new CreateMovieRequest() { Title = "new title", Duration = 90, Genre = "new genre"  };
        var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Movie>(responseString)!;

        request = "/api/v1/Movie/id/" + result.Id;

        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetById_MovieDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Movie/id/88";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
}