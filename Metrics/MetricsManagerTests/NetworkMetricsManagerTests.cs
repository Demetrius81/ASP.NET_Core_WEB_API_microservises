﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkMetricsManagerTests
    {
        private NetworkMetricsController _networkMetricsController;

        public NetworkMetricsManagerTests()
        {
            _networkMetricsController = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;

            TimeSpan fromTime = TimeSpan.FromSeconds(0);

            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _networkMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);

            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _networkMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
