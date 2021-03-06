using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.Jobs;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Source.Converter;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(Configuration
                        .GetSection("Settings:DatabaseOptions:ConnectionString").Value)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations())
                .AddLogging(lg => lg.AddFluentMigratorConsole());

            var mapperConfiguration = new MapperConfiguration(mapperProfile => mapperProfile.AddProfile(new
                MapperProfile()));

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();

            #region MetricsJob

            services.AddSingleton<CpuMetricJob>();

            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * ? * * *"));

            services.AddSingleton<DotNetMetricJob>();

            services.AddSingleton(new JobSchedule(
                typeof(DotNetMetricJob),
                "1/5 * * ? * * *"));

            services.AddSingleton<HddMetricJob>();

            services.AddSingleton(new JobSchedule(
                typeof(HddMetricJob),
                "2/5 * * ? * * *"));

            services.AddSingleton<NetworkMetricJob>();

            services.AddSingleton(new JobSchedule(
                typeof(NetworkMetricJob),
                "3/5 * * ? * * *"));

            services.AddSingleton<RamMetricJob>();

            services.AddSingleton(new JobSchedule(
                typeof(RamMetricJob),
                "4/5 * * ? * * *"));
            #endregion

            services.AddHostedService<QuartzHostedService>();

            #region MetricsRepository

            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            #endregion

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "????? ????? ??????",
                    Description = "????? ????? ???????? ??????",
                    Contact = new OpenApiContact()
                    {
                        Name = "Dmitry Ryzhov",
                        Email = String.Empty,
                        Url = new Uri("https://gb.ru/")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "???????? ?? ??????????????? ???????????? ? 040485 ?? 03 ??????? 2019 ????",
                        Url = new Uri("https://gb.ru/company?tab=license")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.EnableAnnotations();

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            migrationRunner.MigrateUp();
        }
    }
}
