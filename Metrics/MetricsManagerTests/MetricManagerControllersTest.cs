using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTests
{
    public class MetricManagerControllersTest
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        private int _agentId;

        public MetricManagerControllersTest()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);

            _agentId = 1;
        }

        /// <summary>
        /// Тестовый метод для метода GetMetricsFromAgent всех классов, реализующих интерфейс IMetricsManager
        /// </summary>
        [Fact]
        public void AllControllersGetMetricsFromAgentTest_ReturnOk()
        {
            object[] vs = new object[] {_agentId, _fromTime, _toTime };

            Assembly asm = Assembly.LoadFrom("MetricsManager.dll");

            Type[] types = asm.GetTypes();

            foreach (Type type in types)
            {
                if (type.GetInterface("IMetricsManager") is not null)
                {
                    MethodInfo method = type.GetMethod("GetMetricsFromAgent");

                    ConstructorInfo ctоr = type.GetConstructor(Type.EmptyTypes);

                    object instance = ctоr.Invoke(null);

                    object result = method.Invoke(instance, vs);

                    Assert.IsAssignableFrom<IActionResult>(result);
                }
            }
        }

        /// <summary>
        /// Тестовый метод для метода GetMetricsFromAllCluster всех классов, реализующих интерфейс IMetricsManager
        /// </summary>
        [Fact]
        public void AllControllersGetMetricsFromAllClusterTest_ReturnOk()
        {
            object[] vs = new object[] {_fromTime, _toTime };

            Assembly asm = Assembly.LoadFrom("MetricsManager.dll");

            Type[] types = asm.GetTypes();

            foreach (Type type in types)
            {
                if (type.GetInterface("IMetricsManager") is not null)
                {
                    MethodInfo method = type.GetMethod("GetMetricsFromAllCluster");

                    ConstructorInfo ctоr = type.GetConstructor(Type.EmptyTypes);

                    object instance = ctоr.Invoke(null);

                    object result = method.Invoke(instance, vs);

                    Assert.IsAssignableFrom<IActionResult>(result);
                }
            }
        }
    }
}
