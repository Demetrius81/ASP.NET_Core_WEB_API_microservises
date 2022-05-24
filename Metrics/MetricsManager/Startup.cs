using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Models;
using MetricsManager.Models.Interfaces;
using MetricsManager.Services;
using MetricsManager.Services.Interfaces;
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
using Polly;
using Source.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
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

            #region Old

            //services.AddHttpClient();

            //services.AddSingleton<IAgentPool<AgentInfo>, AgentPool>();

            #endregion

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p
                    .WaitAndRetryAsync(retryCount: 3,
                        sleepDurationProvider: (attemptCount) => TimeSpan.FromMilliseconds(2000),
                        onRetry: (exception, sleepDuration, attemptNumber, context) =>
                        {

                        }));

            services.AddSingleton<IAgentsPoolRepository, AgentsPoolRepository>().Configure<DatabaseOptions>(options =>
            {
                Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
            });

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsManager v1"));
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
