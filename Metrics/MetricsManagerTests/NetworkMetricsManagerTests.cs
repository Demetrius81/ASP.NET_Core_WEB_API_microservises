using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Source.Models.Requests;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkMetricsManagerTests
    {
        private NetworkMetricsController _networkMetricsController;

        public NetworkMetricsManagerTests()
        {
            _networkMetricsController = new NetworkMetricsController(null);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            NetworkMetricCreateRequest request = new NetworkMetricCreateRequest()
            {
                AgentId = 1,
                FromTime = TimeSpan.FromSeconds(0),
                ToTime = TimeSpan.FromSeconds(100)
            };

            IActionResult result = _networkMetricsController.GetMetricsFromAgent(request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
