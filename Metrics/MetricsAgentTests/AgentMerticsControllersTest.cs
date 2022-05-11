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

        // �� ������� �������� �������� ��������� �������� ����� ��� ������� ���������� ������ ����������� ������� � �����?

        /// <summary>
        /// �������� ����� ��� ������ GetMetrics ���� �������, ����������� ��������� IMetricsAgent
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

                    ConstructorInfo ct�r = type.GetConstructor(Type.EmptyTypes);

                    object instance = ct�r.Invoke(null);

                    object result = method.Invoke(instance, vs);

                    Assert.IsAssignableFrom<IActionResult>(result);
                }
            }
        }
    }
}
