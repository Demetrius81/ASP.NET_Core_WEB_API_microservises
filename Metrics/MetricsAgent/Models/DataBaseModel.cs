using Source.Models;
using System.Collections.Generic;

namespace MetricsAgent.Models
{
    public static class DataBaseModel
    {
        private static List<CpuMetricDto> _cpuMetricDtoDB;
        private static List<DotNetMetricDto> _dotNetMetricDtoDB;
        private static List<HddMetricDto> _hddMetricDtoDB;
        private static List<NetworkMetricDto> _networkMetricDtoDB;
        private static List<RamMetricDto> _ramMetricDtoDB;

        public static List<CpuMetricDto> CpuMetricDtoDB { get => _cpuMetricDtoDB; set => _cpuMetricDtoDB = value; }
        public static List<DotNetMetricDto> DotNetMetricDtoDB { get => _dotNetMetricDtoDB; set => _dotNetMetricDtoDB = value; }
        public static List<HddMetricDto> HddMetricDtoDB { get => _hddMetricDtoDB; set => _hddMetricDtoDB = value; }
        public static List<NetworkMetricDto> NetworkMetricDtoDB { get => _networkMetricDtoDB; set => _networkMetricDtoDB = value; }
        public static List<RamMetricDto> RamMetricDtoDB { get => _ramMetricDtoDB; set => _ramMetricDtoDB = value; }
    }
}
