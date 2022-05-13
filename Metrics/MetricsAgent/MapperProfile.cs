using AutoMapper;
using MetricsAgent.Models;
using System;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<DotNetMetric, DotNetMetricDto>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<NetworkMetric, NetworkMetricDto>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<HddMetric, HddMetricDto>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<RamMetric, RamMetricDto>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));
        }
    }
}
