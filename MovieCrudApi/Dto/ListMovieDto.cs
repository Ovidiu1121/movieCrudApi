namespace MovieCrudApi.Dto;

public class ListMovieDto
{
    public ListMovieDto()
    {
        movieList = new List<MovieDto>();
    }
    
    public List<MovieDto> movieList { get; set; }
}