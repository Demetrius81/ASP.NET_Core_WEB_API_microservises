using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Source.Models.Requests;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsManagerTests
    {
        private CpuMetricsController _cpuMetricsController;

        public CpuMetricsManagerTests()
        {
            _cpuMetricsController = new CpuMetricsController(null);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            CpuMetricCreateRequest request = new CpuMetricCreateRequest()
            {
                AgentId = 1,
                FromTime = TimeSpan.FromSeconds(0),
                ToTime = TimeSpan.FromSeconds(100)
            };            

            IActionResult result = _cpuMetricsController.GetMetricsFromAgent(request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
