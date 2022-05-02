using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkControllerTests
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        public NetworkControllerTests()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);
        }

        [Fact]
        public void NetworkControllerTest()
        {
            IMetricsController controller = new NetworkMetricsController();

            IActionResult result = controller.GetMetrics(_fromTime, _toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
