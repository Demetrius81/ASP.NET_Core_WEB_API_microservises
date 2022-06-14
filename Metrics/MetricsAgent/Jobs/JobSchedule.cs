using System;

namespace MetricsAgent.Jobs
{
    public class JobSchedule
    {
        /// <summary>
        /// Тип задачи
        /// </summary>
        public Type JobType { get; init; }

        /// <summary>
        /// Выражение, описывающее периодичность вызова задачи
        /// </summary>
        public string CronExpression { get; init; }

        public JobSchedule(Type jobType, string croneExpression)
        {
            JobType = jobType;

            CronExpression = croneExpression;
        }
    }
}
