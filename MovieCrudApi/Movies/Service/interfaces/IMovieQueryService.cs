using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Service.interfaces
{
    public interface IMovieQueryService
    {
        Task<ListMovieDto> GetAll();
        Task<MovieDto> GetByTitle(string title);
        Task<MovieDto> GetById(int id);
    }
}
