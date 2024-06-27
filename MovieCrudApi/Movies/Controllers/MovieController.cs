using Microsoft.AspNetCore.Mvc;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Controllers.interfaces;
using MovieCrudApi.Movies.Model;
using MovieCrudApi.Movies.Repository.interfaces;
using MovieCrudApi.Movies.Service.interfaces;
using MovieCrudApi.System.Exceptions;

namespace MovieCrudApi.Movies.Controllers
{  
    public class MovieController: MovieApiController
    {
        private IMovieCommandService _movieCommandService;
        private IMovieQueryService _movieQueryService;

        public MovieController(IMovieCommandService movieCommandService, IMovieQueryService movieQueryService)
        {
            _movieCommandService = movieCommandService;
            _movieQueryService = movieQueryService;
        }

        public override async Task<ActionResult<MovieDto>> CreateMovie([FromBody] CreateMovieRequest request)
        {
            try
            {
                var movie = await _movieCommandService.CreateMovie(request);

                return Created("Filmul a fost adaugat",movie);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<MovieDto>> DeleteMovie([FromRoute] int id)
        {
            try
            {
                var movie = await _movieCommandService.DeleteMovie(id);

                return Accepted("", movie);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListMovieDto>> GetAll()
        {
            try
            {
                var movie = await _movieQueryService.GetAll();
                return Ok(movie);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<MovieDto>> GetByIdRoute([FromRoute] int id)
        {
            try
            {
                var movie = await _movieQueryService.GetById(id);
                return Ok(movie);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<MovieDto>> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieRequest request)
        {
            try
            {
                var movie = await _movieCommandService.UpdateMovie(id, request);

                return Ok(movie);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
