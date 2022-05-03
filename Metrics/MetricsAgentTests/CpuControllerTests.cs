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
    public class CpuControllerTests
    {
        private Mock<ICpuMetricsRepository> _mockMetricsRepository;

        private IMetricsController _controller;

        public CpuControllerTests()
        {
            _mockMetricsRepository = new Mock<ICpuMetricsRepository>();

            _controller = new CpuMetricsController(null, _mockMetricsRepository.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {           
            _mockMetricsRepository.Setup(repository => 
                repository.Create(It.IsAny<IMetric>())).Verifiable();

            IActionResult result = _controller.Create(new CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            _mockMetricsRepository.Verify(repository => 
                repository.Create(It.IsAny<IMetric>()), Times.AtMostOnce());
        }
    }
}
