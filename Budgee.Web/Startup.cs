<<<<<<< HEAD
using System.Collections.Generic;
using Budgee.DailyBudgets;
using Budgee.DailyBudgets.Messages;
using Budgee.Framework;
using Budgee.Infrastructure;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgee.DailyBudgets;
using Budgee.DailyBudgets.DailyBudgets;
using Budgee.Domain.DailyBudgets;
using Budgee.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Logging;
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be

namespace Budgee.Web
{
    public class Startup
    {
<<<<<<< HEAD
        public Startup(IHostEnvironment environment,IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IHostEnvironment Environment{ get; }

=======
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< HEAD
            

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
=======
            services.AddSingleton<IDailyBudgetRepository, InMemoryRepository>()
                    .AddSingleton<IApplicationService, DailyBudgetsCommandService>()
                    .AddControllers();
                   services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DailyBudgets", Version = "v1" }));
            
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
<<<<<<< HEAD
            //app.UseHttpsRedirection();

            app.UseRouting();

            ////app.UseAuthorization();
=======

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DailyBudgets v1"));
        }
    }
}
