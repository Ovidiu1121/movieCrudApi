using Microsoft.AspNetCore.Mvc;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class MovieApiController: ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Movie>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListMovieDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<MovieDto>> CreateMovie([FromBody] CreateMovieRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<MovieDto>> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<MovieDto>> DeleteMovie([FromRoute] int id);

        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<MovieDto>> GetByIdRoute([FromRoute] int id);
    }
}
