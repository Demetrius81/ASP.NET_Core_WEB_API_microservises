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
    public class DotNetControllerTests
    {
        private Mock<IDotNetMetricsRepository> _mockMetricsRepository;

        private IMetricsController _controller;

        public DotNetControllerTests()
        {
            _mockMetricsRepository = new Mock<IDotNetMetricsRepository>();

            _controller = new DotNetMetricsController(null, _mockMetricsRepository.Object);
        }

        [Fact]
        public void Create_SendRequest_ShouldReturnOk()
        {           
            _mockMetricsRepository.Setup(repository =>
                repository.Create(It.IsAny<IMetric>())).Verifiable();
            
            IActionResult result = _controller.Create(new DotNetMetricCreateRequest
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
