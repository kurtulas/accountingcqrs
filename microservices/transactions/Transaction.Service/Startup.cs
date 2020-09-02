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
using Transaction.Service.CommandServices;
using Accounting.Domain.Application.CommandServices;
using Accounting.Domain.Business.Transactions;
using Accounting.Domain.Application.QueryServices;
using Transaction.ReadModel.Module;
using Transaction.ReadModel.Services;
using EventFlow.Subscribers;
using EventFlow.Aggregates;
using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Events;
using System.Threading;
using EventFlow.RabbitMQ.Extensions;
using EventFlow.RabbitMQ;
using Infrastructure.RabbitMq;
using Infrastructure.Configurations;

namespace Transaction.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public IEventFlowOptions Options { get; set; }

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
                ef.PublishToRabbitMq(
                    RabbitMqConfiguration.With(new Uri(envconfig.RabbitMqConnection),
                        true, 5, envconfig.RabbitMqConnection));
                ef.AddAspNetCore();
                ef.UseConsoleLog();
                ef.RegisterModule<DomainModule>();
                ef.RegisterModule<TransactionReadModelModule>();
                ef.Configure(cfg => cfg.IsAsynchronousSubscribersEnabled = true);
                ef.RegisterServices(s =>
                {
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction API V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            //var environmentConfig = app.ApplicationServices.GetService<IRabbitMqSubscriber>();
            var subscriber = app.ApplicationServices.GetService<IRabbitMqSubscriber>();
            var configuration = app.ApplicationServices.GetService<IRabbitMqConfiguration>();
            var domainEventPublisher = app.ApplicationServices.GetService<IDomainEventPublisher>();

            subscriber.SubscribeAsync(
                "eventflow",
                "eventflowQueue",
                EventFlowRabbitExtensions.Listen,
                domainEventPublisher,
                cancellationToken: CancellationToken.None)
                .Wait();

        }
    }
}
