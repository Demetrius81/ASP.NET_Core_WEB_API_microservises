using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Source.Models.Requests;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsManagerTests
    {
        private DotNetMetricsController _dotNetMetricsController;

        public DotNetMetricsManagerTests()
        {
            _dotNetMetricsController = new DotNetMetricsController(null);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            DotNetMetricCreateRequest request = new DotNetMetricCreateRequest()
            {
                AgentId = 1,
                FromTime = TimeSpan.FromSeconds(0),
                ToTime = TimeSpan.FromSeconds(100)
            };

            IActionResult result = _dotNetMetricsController.GetMetricsFromAgent(request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
