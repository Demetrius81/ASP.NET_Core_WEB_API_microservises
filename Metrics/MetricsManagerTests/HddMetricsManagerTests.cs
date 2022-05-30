using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Source.Models.Requests;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class HddMetricsManagerTests
    {
        private HddMetricsController _hddMetricsController;

        public HddMetricsManagerTests()
        {
            _hddMetricsController = new HddMetricsController(null);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            HddMetricCreateRequest request = new HddMetricCreateRequest()
            {
                AgentId = 1,
                FromTime = TimeSpan.FromSeconds(0),
                ToTime = TimeSpan.FromSeconds(100)
            };

            IActionResult result = _hddMetricsController.GetMetricsFromAgent(request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }        
    }
}
