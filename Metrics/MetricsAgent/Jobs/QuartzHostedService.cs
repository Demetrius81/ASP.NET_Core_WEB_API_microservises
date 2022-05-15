using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;

        private readonly IJobFactory _jobFactory;

        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public IScheduler Scheduler { get; set; }

        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<JobSchedule> jobSchedules)
        {
            _jobFactory = jobFactory;

            _schedulerFactory = schedulerFactory;

            _jobSchedules = jobSchedules;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {            
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                IJobDetail job = CreateJobDetail(jobSchedule);

                ITrigger trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private ITrigger CreateTrigger(JobSchedule jobSchedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{jobSchedule.JobType.FullName}.trigger")
                .WithCronSchedule(jobSchedule.CronExpression)
                .WithDescription(jobSchedule.CronExpression)
                .Build();
        }

        private IJobDetail CreateJobDetail(JobSchedule jobSchedule)
        {
            Type jobType = jobSchedule.JobType;

            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }
    }
}
