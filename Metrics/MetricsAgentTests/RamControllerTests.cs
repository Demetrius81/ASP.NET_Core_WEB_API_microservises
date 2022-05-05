using MetricsAgent.Controllers;
using MetricsAgent.Models.Interfaces;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerTests
    {
        private Mock<IRamMetricsRepository> _mockMetricsRepository;

        private IMetricsController _controller;

        public RamControllerTests()
        {
            _mockMetricsRepository = new Mock<IRamMetricsRepository>();

            _controller = new RamMetricsController(null, _mockMetricsRepository.Object);
        }

        [Fact]
        public void Create_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                repository.Create(It.IsAny<IMetric>())).Verifiable();

            IActionResult result = _controller.Create(new RamMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mockMetricsRepository.Verify(repository =>
                repository.Create(It.IsAny<IMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAll_SendRequest_ShouldReturnOk()
        {

        }

        [Fact]
        public void GetMetrics_SendRequest_ShouldReturnOk()
        {

        }
    }
}
