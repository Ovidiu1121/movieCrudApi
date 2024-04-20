using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Repository.interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> GetByTitleAsync(string title);
        Task<Movie> CreateMovie(CreateMovieRequest request);
        Task<Movie> UpdateMovie(int id, UpdateMovieRequest request);
        Task<Movie> DeleteMovieById(int id);
    }
}
