using AutoMapper;
using Service.History.DataAccess.Entities;
using Service.History.Infrastructure.Dto;

namespace Service.History.Infrastructure.Profiles
{
    public class CarAutoMapperProfile: Profile
    {
        public CarAutoMapperProfile()
        {
            CreateMap<CarEntity, CarDto>().ReverseMap();
        }
    }
}
