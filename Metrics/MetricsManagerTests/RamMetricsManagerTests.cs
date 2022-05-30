using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Source.Models.Requests;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsManagerTests
    {
        private RamMetricsController _ramMetricsController;

        public RamMetricsManagerTests()
        {
            _ramMetricsController = new RamMetricsController(null);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            RamMetricCreateRequest request = new RamMetricCreateRequest()
            {
                AgentId = 1,
                FromTime = TimeSpan.FromSeconds(0),
                ToTime = TimeSpan.FromSeconds(100)
            };

            IActionResult result = _ramMetricsController.GetMetricsFromAgent(request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
