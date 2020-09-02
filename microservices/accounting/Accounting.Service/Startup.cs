using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using EventFlow;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.AspNetCore.Extensions;
using Accounting.Domain.Module;
using Accounting.Service.CommandServices;
using Accounting.Domain.Application.CommandServices;
using Accounting.ReadModel.Module;
using Accounting.Domain.Business.Transactions.Commands;
using EventFlow.Hangfire.Extensions;
using Hangfire;
using Hangfire.MemoryStorage;
using EventFlow.RabbitMQ.Extensions;
using EventFlow.RabbitMQ;
using Infrastructure.Configurations;

namespace Accounting.Service
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
            services.AddMvc();
            services.AddControllers();
            ConfigureSwagger(services);
            services.AddAutoMapper(typeof(Startup));
            services.AddEventFlow(ef =>
            {
                var envconfig = EnvironmentConfiguration.Bind(Configuration);
                services.AddSingleton(envconfig);
                ef.AddDefaults(typeof(Startup).Assembly);
                //ef.Configure(cfg => cfg.IsAsynchronousSubscribersEnabled = true);
                ef.PublishToRabbitMq(
                   RabbitMqConfiguration.With(new Uri(envconfig.RabbitMqConnection),
                       true, 5, envconfig.RabbitExchange));
                ef.AddAspNetCore();
                //ef.UseHangfireJobScheduler();
                ef.UseConsoleLog();
                ef.RegisterModule<DomainModule>();
                ef.RegisterModule<AccountingReadModelModule>();                
                ef.RegisterServices(s =>
                {
                    s.Register<ICustomerCommandService, CustomerCommandService>();
                    s.Register<IAccountCommandService, AccountCommandService>();
                    s.Register<ITransactionCommandService, TransactionCommandService>();
                });
            });        
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Accounting API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c =>{ c.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounting API V1");})
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
