using AutoMapper;
using PropertiesMinimalAPI.Models;
using PropertiesMinimalAPI.Models.DTOS;

namespace PropertiesMinimalAPI.Maps
{
    public class CustomMap: Profile
    {
        public CustomMap()
        {
            CreateMap<Properties, CreatePropertyDTO>().ReverseMap();
            CreateMap<Properties, PropertyDTO>().ReverseMap();
        }
    }
}
