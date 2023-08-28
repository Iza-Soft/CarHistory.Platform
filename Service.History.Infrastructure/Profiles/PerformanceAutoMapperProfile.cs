using AutoMapper;
using Service.History.DataAccess.Entities;
using Service.History.Infrastructure.Dto;

namespace Service.History.Infrastructure.Profiles
{
    public class PerformanceAutoMapperProfile: Profile
    {
        public PerformanceAutoMapperProfile()
        {
            //CreateMap<PerformanceEntity, PerformanceDto>().ReverseMap();
        }
    }
}
