using AutoMapper;
using MovieCrudApi.Dto;
using MovieCrudApi.Movies.Model;

namespace MovieCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {

            CreateMap<CreateMovieRequest, Movie>();
            CreateMap<UpdateMovieRequest, Movie>();

        }

    }
}
