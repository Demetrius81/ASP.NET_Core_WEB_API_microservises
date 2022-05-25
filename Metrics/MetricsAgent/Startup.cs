using AutoMapper;
using MetricsAgent.Converter;
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
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
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
            var mapperConfiguration = new MapperConfiguration(mapperProfile => mapperProfile.AddProfile(new
                MapperProfile()));

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);

            ConfigureSqlLiteConnection(services);


            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>().Configure<DatabaseOptions>(options =>
                {
                    Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
                });

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";

            SQLiteConnection connection = new SQLiteConnection(connectionString);

            connection.Open();

            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                List<string> TabNames = new List<string>()
                {
                    "cpumetrics",
                    "dotnetmetrics",
                    "hddmetrics",
                    "networkmetrics",
                    "rammetrics"
                };

                foreach (string TabName in TabNames)
                {
                    command.CommandText = $"DROP TABLE IF EXISTS {TabName}";

                    command.ExecuteNonQuery();

                    command.CommandText = $@"CREATE TABLE {TabName}(
                    id INTEGER PRIMARY KEY,
                    value INT, 
                    time INT)";

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
