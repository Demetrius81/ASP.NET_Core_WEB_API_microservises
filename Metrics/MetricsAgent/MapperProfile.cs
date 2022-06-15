using AutoMapper;
using Source.Models;
using System;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricDto, CpuMetric>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<DotNetMetricDto, DotNetMetric>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<NetworkMetricDto, NetworkMetric>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<HddMetricDto, HddMetric>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));

            CreateMap<RamMetricDto, RamMetric>().ForMember(metric =>
                metric.Time, options =>
                options.MapFrom(source =>
                TimeSpan.FromSeconds(source.Time)));
        }
    }
}
