using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Service.interfaces
{
    public interface IMovieCommandService
    {
        Task<MovieDto> CreateMovie(CreateMovieRequest request);
        Task<MovieDto> UpdateMovie(int id, UpdateMovieRequest request);
        Task<MovieDto> DeleteMovie(int id);

    }
}
