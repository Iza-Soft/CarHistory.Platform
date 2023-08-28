using AutoMapper;
using Service.History.DataAccess.Entities;
using Service.History.Infrastructure.Dto;

namespace Service.History.Infrastructure.Profiles
{
    public class EngineAutoMapperProfile: Profile
    {
        public EngineAutoMapperProfile()
        {
            //CreateMap<EngineEntity, EngineDto>().ReverseMap();
        }
    }
}
