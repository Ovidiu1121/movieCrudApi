using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCrudApi.Data;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;
using MovieCrudApi.Movies.Repository.interfaces;

namespace MovieCrudApi.Movies.Repository
{
    public class MovieRepository:IMovieRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Movie> CreateMovie(CreateMovieRequest request)
        {
            var movie = _mapper.Map<Movie>(request);

            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> DeleteMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
           return await _context.Movies.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Movie> GetByTitleAsync(string title)
        {
            return await _context.Movies.SingleOrDefaultAsync(x => x.Title.Equals(title));
        }

        public async Task<Movie> UpdateMovie(int id, UpdateMovieRequest request)
        {
            var movie = await _context.Movies.FindAsync(id);

            movie.Title= request.Title ?? movie.Title;
            movie.Duration= request.Duration ?? movie.Duration;
            movie.Genre=request.Genre ?? movie.Genre;

            _context.Movies.Update(movie);

            await _context.SaveChangesAsync();

            return movie;
        }
    }
}
