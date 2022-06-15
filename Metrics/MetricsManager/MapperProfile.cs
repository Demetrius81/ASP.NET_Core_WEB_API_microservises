using AutoMapper;
using MetricsManager.Models;
using System;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentInfo, AgentInfoDto>().ForMember(x =>
                x.AgentAddress, options =>
                options.MapFrom(source =>
                source.AgentAddress.ToString()));

            CreateMap<AgentInfoDto, AgentInfo>().ForMember(x =>
                x.AgentAddress, options =>
                options.MapFrom(source =>
                new Uri(source.AgentAddress)));
        }
    }
}
