using AirLines.ApiModel;
using AirLines.Models;
using AutoMapper;

namespace AirLines.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AirLineApiModel, AirlineViewModel>().ReverseMap();
        }
    }
}
