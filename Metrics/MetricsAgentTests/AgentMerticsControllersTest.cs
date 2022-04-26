using System;
using System.Reflection;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;

namespace MetricsAgentTests
{
    public class AgentMerticsControllersTest
    {

        [Fact]
        public void TestControllers()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);

            TimeSpan toTime = TimeSpan.FromSeconds(2);

            object[] vs = new object[] { fromTime, toTime };

            Assembly asm = Assembly.LoadFrom("MetricsAgent.dll");

            Type[] types = asm.GetTypes();

            foreach (Type type in types)
            {
                if (type.GetInterface("IMetricsAgent") is not null)
                {
                    MethodInfo method = type.GetMethod("GetMetrics");

                    ConstructorInfo ctr = type.GetConstructor(Type.EmptyTypes);

                    object instance = ctr.Invoke(null);

                    object result = method.Invoke(instance, vs);

                    Assert.IsAssignableFrom<IActionResult>(result);
                }
            }            
        }
    }
}
