using System;
using System.Net.Http;

namespace MetricsManager.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Logic logic = new Logic();

            logic.ShowMetrics();
        }
    }
}
