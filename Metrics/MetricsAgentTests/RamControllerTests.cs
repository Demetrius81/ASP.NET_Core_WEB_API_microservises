using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerTests
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        public RamControllerTests()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);
        }

        [Fact]
        public void RamControllerTest()
        {
            IMetricsController controller = new RamMetricsController();

            IActionResult result = controller.GetMetrics(_fromTime, _toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
