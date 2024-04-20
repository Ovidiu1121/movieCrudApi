using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Service.interfaces
{
    public interface IMovieCommandService
    {
        Task<Movie> CreateMovie(CreateMovieRequest request);
        Task<Movie> UpdateMovie(int id, UpdateMovieRequest request);
        Task<Movie> DeleteMovie(int id);

    }
}
