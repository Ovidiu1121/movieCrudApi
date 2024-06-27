using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Repository.interfaces
{
    public interface IMovieRepository
    {
        Task<ListMovieDto> GetAllAsync();
        Task<MovieDto> GetByIdAsync(int id);
        Task<MovieDto> GetByTitleAsync(string title);
        Task<MovieDto> CreateMovie(CreateMovieRequest request);
        Task<MovieDto> UpdateMovie(int id, UpdateMovieRequest request);
        Task<MovieDto> DeleteMovieById(int id);
    }
}
