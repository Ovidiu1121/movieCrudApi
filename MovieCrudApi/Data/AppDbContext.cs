using Microsoft.EntityFrameworkCore;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Movie> Movies { get; set; }

    }
}
