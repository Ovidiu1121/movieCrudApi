
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;
using MovieCrudApi.Movies.Repository;
using MovieCrudApi.Movies.Repository.interfaces;
using MovieCrudApi.Movies.Service.interfaces;
using MovieCrudApi.System.Exceptions;
using MovieCrudApi.System.Constant;

namespace MovieCrudApi.Movies.Service
{
    public class MovieCommandService: IMovieCommandService
    {
        private IMovieRepository _repository;

        public MovieCommandService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovieDto> CreateMovie(CreateMovieRequest request)
        {
            MovieDto movie = await _repository.GetByTitleAsync(request.Title);

            if (movie!=null)    
            {
                throw new ItemAlreadyExists(System.Constant.Constants.MOVIE_ALREADY_EXIST);
            }

            movie=await _repository.CreateMovie(request);
            return movie;
        }

        public async Task<MovieDto> DeleteMovie(int id)
        {
            MovieDto movie = await _repository.GetByIdAsync(id);

            if (movie==null)
            {
                throw new ItemDoesNotExist(System.Constant.Constants.MOVIE_DOES_NOT_EXIST);
            }

            await _repository.DeleteMovieById(id);
            return movie;
        }

        public async Task<MovieDto> UpdateMovie(int id, UpdateMovieRequest request)
        {
            MovieDto movie = await _repository.GetByIdAsync(id);

            if (movie==null)
            {
                throw new ItemDoesNotExist(System.Constant.Constants.MOVIE_DOES_NOT_EXIST);
            }

            movie = await _repository.UpdateMovie(id, request);
            return movie;
        }
    }
}
