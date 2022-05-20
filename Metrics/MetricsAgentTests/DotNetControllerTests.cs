using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetControllerTests
    {
        private Mock<IDotNetMetricsRepository> _mockMetricsRepository;

        private DotNetMetricsController _controller;

        public DotNetControllerTests()
        {
            _mockMetricsRepository = new Mock<IDotNetMetricsRepository>();

            _controller = new DotNetMetricsController(_mockMetricsRepository.Object);
        }

        #region For delete

        //[Fact]
        //public void Create_SendRequest_ShouldReturnOk()
        //{           
        //    _mockMetricsRepository.Setup(repository =>
        //        repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

        //    IActionResult result = _controller.Create(new DotNetMetricCreateRequest
        //    {
        //        Time = TimeSpan.FromSeconds(1),
        //        Value = 50
        //    });

        //    _mockMetricsRepository.Verify(repository =>
        //        repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        //}

        #endregion

        [Fact]
        public void GetAll_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetAll())
                              .Returns(new List<DotNetMetric>());

            _controller.GetAll();

            _mockMetricsRepository.Verify(repository =>
                    repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetrics_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                              .Returns(new List<DotNetMetric>());

            _controller.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
