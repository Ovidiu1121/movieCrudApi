using MovieCrudApi.Dto;

namespace tests.Helpers;

public class TestMovieFactory
{
    public static MovieDto CreateMovie(int id)
    {
        return new MovieDto
        {
            Id = id,
            Title="Spiderman"+id,
            Duration=120+id,
            Genre="Action"+id
        };
    }

    public static ListMovieDto CreateMovies(int count)
    {
        ListMovieDto movies=new ListMovieDto();
            
        for(int i = 0; i<count; i++)
        {
            movies.movieList.Add(CreateMovie(i));
        }
        return movies;
    }
}