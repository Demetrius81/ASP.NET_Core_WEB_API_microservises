using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuControllerTests
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        public CpuControllerTests()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);
        }

        [Fact]
        public void CpuControllerTest()
        {
            IMetricsAgent controller = new CpuMetricsController();

            IActionResult result = controller.GetMetrics(_fromTime, _toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
