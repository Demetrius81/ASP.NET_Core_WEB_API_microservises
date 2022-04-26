using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    public interface IMetricsManager
    {
        IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);

        IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);
    }
}
