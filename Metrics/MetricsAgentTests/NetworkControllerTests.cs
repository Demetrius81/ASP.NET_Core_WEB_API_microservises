using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkControllerTests
    {
        private Mock<INetworkMetricsRepository> _mockMetricsRepository;

        private NetworkMetricsController _controller;

        public NetworkControllerTests()
        {
            _mockMetricsRepository = new Mock<INetworkMetricsRepository>();

            _controller = new NetworkMetricsController(_mockMetricsRepository.Object);
        }

        #region For delete

        //[Fact]
        //public void Create_SendRequest_ShouldReturnOk()
        //{
        //    _mockMetricsRepository.Setup(repository =>
        //        repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

        //    IActionResult result = _controller.Create(new NetworkMetricCreateRequest
        //    {
        //        Time = TimeSpan.FromSeconds(1),
        //        Value = 50
        //    });

        //    _mockMetricsRepository.Verify(repository =>
        //        repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        //}

        #endregion

        [Fact]
        public void GetAll_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetAll())
                              .Returns(new List<NetworkMetric>());

            _controller.GetAll();

            _mockMetricsRepository.Verify(repository =>
                    repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetrics_SendRequest_ShouldReturnOk()
        {
            _mockMetricsRepository.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                              .Returns(new List<NetworkMetric>());

            _controller.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            _mockMetricsRepository.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
