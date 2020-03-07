using System.Collections.Generic;
using Budgee.DailyBudgets;
using Budgee.DailyBudgets.Messages;
using Budgee.Framework;
using Budgee.Infrastructure;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Budgee.Web
{
    public class Startup
    {
        public Startup(IHostEnvironment environment,IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IHostEnvironment Environment{ get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            var esConnection = EventStoreConnection.Create(
                Configuration["eventStore:connectionString"], 
                ConnectionSettings.Create().KeepReconnecting(), 
                Environment.ApplicationName);
            var store = new EsAggregateStore(esConnection);
            services.AddSingleton(esConnection)
                    .AddSingleton<IAggregateStore>(store)
                    .AddSingleton<IApplicationService, DailyBudgetsCommandService>()
                    .AddControllers();

            var dailybudgetItems = new List<ReadModels.DailyBudgets>();
            services.AddSingleton<IEnumerable<ReadModels.DailyBudgets>>(dailybudgetItems);
            var subscription = new EsSubscription(esConnection, dailybudgetItems);
            services.AddSingleton<IHostedService>(new EventStoreService(esConnection,subscription));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DailyBudgets", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();

            app.UseRouting();

            ////app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DailyBudgets v1"));
        }
    }
}
