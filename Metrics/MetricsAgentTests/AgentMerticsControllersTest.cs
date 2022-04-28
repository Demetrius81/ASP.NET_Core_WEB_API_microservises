using System;
using System.Reflection;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class AgentMerticsControllersTest
    {
        private TimeSpan _fromTime;

        private TimeSpan _toTime;

        public AgentMerticsControllersTest()
        {
            _fromTime = TimeSpan.FromSeconds(0);

            _toTime = TimeSpan.FromSeconds(2);
        }

        // На сколько валидным является следующий тестовый метод при условии одинаковой работы тестируемых методов и теста?

        /// <summary>
        /// Тестовый метод для метода GetMetrics всех классов, реализующих интерфейс IMetricsAgent
        /// </summary>
        [Fact]
        public void AllControllersTest()
        {
            object[] vs = new object[] { _fromTime, _toTime };

            Assembly asm = Assembly.LoadFrom("MetricsAgent.dll");

            Type[] types = asm.GetTypes();

            foreach (Type type in types)
            {
                if (type.GetInterface("IMetricsAgent") is not null)
                {
                    MethodInfo method = type.GetMethod("GetMetrics");

                    ConstructorInfo ctоr = type.GetConstructor(Type.EmptyTypes);

                    object instance = ctоr.Invoke(null);

                    object result = method.Invoke(instance, vs);

                    Assert.IsAssignableFrom<IActionResult>(result);
                }
            }
        }
    }
}
