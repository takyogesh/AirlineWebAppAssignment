using AirlineWebApp.ApiModel;
using AirlineWebApp.Models;
using AutoMapper;

namespace AirlineWebApp.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AirLineApiModel, AirlineViewModel>().ReverseMap();
        }
    }
}
