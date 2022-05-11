using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddControllerTests
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        public HddControllerTests()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);
        }

        [Fact]
        public void HddControllerTest()
        {
            IMetricsAgent controller = new HddMetricsController();

            IActionResult result = controller.GetMetrics(_fromTime, _toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
