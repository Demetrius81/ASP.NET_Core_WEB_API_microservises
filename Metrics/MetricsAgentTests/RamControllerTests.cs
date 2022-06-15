using MetricsAgent.Controllers;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Source.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerTests
    {
        private Mock<IRamMetricsRepository> _mockMetricsRepository;

        private RamMetricsController _controller;

        public RamControllerTests()
        {
            _mockMetricsRepository = new Mock<IRamMetricsRepository>();

            _controller = new RamMetricsController(_mockMetricsRepository.Object);
        }

        #region For delete

        //[Fact]
        //public void Create_SendRequest_ShouldReturnOk()
        //{
        //    _mockMetricsRepository.Setup(repository =>
        //        repository.Create(It.IsAny<RamMetric>())).Verifiable();

        //    IActionResult result = _controller.Create(new RamMetricCreateRequest
        //    {
        //        Time = TimeSpan.FromSeconds(1),
        //        Value = 50
        //    });

        //    _mockMetricsRepository.Verify(repository =>
        //        repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        //}

        #endregion

        [Fact]
        public void GetAll_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetAll())
                              .Returns(new List<RamMetricDto>());

            _controller.GetAll();

            _mockMetricsRepository.Verify(repository =>
                    repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetrics_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                              .Returns(new List<RamMetricDto>());

            _controller.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
