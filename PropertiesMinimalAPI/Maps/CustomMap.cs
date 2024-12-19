using AutoMapper;
using PropertiesMinimalAPI.Models;
using PropertiesMinimalAPI.Models.DTOS;

namespace PropertiesMinimalAPI.Maps
{
    public class CustomMap: Profile
    {
        public CustomMap()
        {
            CreateMap<CreatePropertyDTO, Properties>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<Properties, PropertyDTO>().ReverseMap();
        }
    }
}
