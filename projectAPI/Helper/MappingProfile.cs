using AutoMapper;

namespace projectAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailesDTO>();
            CreateMap<MovieCreateDTO, Movie>()
                   .ForMember(src => src.Poster, opt => opt.Ignore());
           

        }
    }
}
