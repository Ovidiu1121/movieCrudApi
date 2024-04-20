using MovieCrudApi.Movies.Model;
using MovieCrudApi.Movies.Repository;
using MovieCrudApi.Movies.Repository.interfaces;
using MovieCrudApi.Movies.Service.interfaces;
using MovieCrudApi.System.Constant;
using MovieCrudApi.System.Exceptions;

namespace MovieCrudApi.Movies.Service
{
    public class MovieQueryService: IMovieQueryService
    {

        private IMovieRepository _repository;

        public MovieQueryService(MovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            IEnumerable<Movie> movies = await _repository.GetAllAsync();

            if (movies.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_MOVIES_EXIST);
            }

            return movies;
        }

        public async Task<Movie> GetById(int id)
        {
            Movie movies = await _repository.GetByIdAsync(id);

            if (movies == null)
            {
                throw new ItemDoesNotExist(Constants.MOVIE_DOES_NOT_EXIST);
            }

            return movies;
        }

        public async Task<Movie> GetByTitle(string title)
        {
            Movie movies = await _repository.GetByTitleAsync(title);

            if (movies == null)
            {
                throw new ItemDoesNotExist(Constants.MOVIE_DOES_NOT_EXIST);
            }

            return movies;
        }
    }
}
