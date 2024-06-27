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

        public async Task<MovieDto> CreateMovie(CreateMovieRequest request)
        {
            var movie = _mapper.Map<Movie>(request);

            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> DeleteMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<ListMovieDto> GetAllAsync()
        {
            List<Movie> result = await _context.Movies.ToListAsync();
            
            ListMovieDto listDoctorDto = new ListMovieDto()
            {
                movieList = _mapper.Map<List<MovieDto>>(result)
            };

            return listDoctorDto;
        }

        public async Task<MovieDto> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> GetByTitleAsync(string title)
        {
            var movie = await _context.Movies.Where(m => m.Title.Equals(title)).FirstOrDefaultAsync();
            
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMovie(int id, UpdateMovieRequest request)
        {
            var movie = await _context.Movies.FindAsync(id);

            movie.Title= request.Title ?? movie.Title;
            movie.Duration= request.Duration ?? movie.Duration;
            movie.Genre=request.Genre ?? movie.Genre;

            _context.Movies.Update(movie);

            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }
    }
}
