﻿using MetricsAgent.Models.Interfaces;
using System;

namespace MetricsAgent.Models
{
    public class CpuMetricDto : IMetricDto
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}