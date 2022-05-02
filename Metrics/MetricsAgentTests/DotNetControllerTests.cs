using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetControllerTests
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        public DotNetControllerTests()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);
        }

        [Fact]
        public void DotNetControllerTest()
        {
            IMetricsController controller = new DotNetMetricsController();

            IActionResult result = controller.GetMetrics(_fromTime, _toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
