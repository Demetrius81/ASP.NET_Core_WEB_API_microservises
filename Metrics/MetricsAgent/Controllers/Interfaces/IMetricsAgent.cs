using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    public interface IMetricsAgent
    {        
        IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);
    }
}
