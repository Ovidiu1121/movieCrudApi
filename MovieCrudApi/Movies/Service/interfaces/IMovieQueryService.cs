using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Movies.Service.interfaces
{
    public interface IMovieQueryService
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetByTitle(string title);
        Task<Movie> GetById(int id);
    }
}
