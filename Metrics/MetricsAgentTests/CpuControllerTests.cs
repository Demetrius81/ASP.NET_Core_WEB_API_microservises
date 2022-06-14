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
    public class CpuControllerTests
    {
        private Mock<ICpuMetricsRepository> _mockMetricsRepository;
        
        private CpuMetricsController _controller;

        public CpuControllerTests()
        {
            _mockMetricsRepository = new Mock<ICpuMetricsRepository>();

            _controller = new CpuMetricsController(_mockMetricsRepository.Object);
        }

        #region For delete

        //[Fact]
        //public void  Create_SendRequest_ShouldReturnOk()
        //{
        //    _mockMetricsRepository.Setup(repository =>
        //        repository.Create(It.IsAny<CpuMetric>())).Verifiable();

        //    IActionResult result = _controller.Create(new CpuMetricCreateRequest
        //    {
        //        Time = TimeSpan.FromSeconds(1),
        //        Value = 50
        //    });

        //    _mockMetricsRepository.Verify(repository =>
        //        repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        //}

        #endregion

        [Fact]
        public void GetAll_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetAll())
                              .Returns(new List<CpuMetric>());

            _controller.GetAll();

            _mockMetricsRepository.Verify(repository =>
                    repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetrics_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                              .Returns(new List<CpuMetric>());

            _controller.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
