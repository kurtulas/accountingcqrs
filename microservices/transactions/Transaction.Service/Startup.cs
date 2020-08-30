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
using Transaction.ReadModel.Module;
using Transaction.Service.CommandServices;
using Accounting.Domain.Application.CommandServices;

namespace Transaction.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            ConfigureSwagger(services);
            services.AddAutoMapper(typeof(Startup));
            services.AddEventFlow(ef =>
            {
                ef.AddDefaults(typeof(Startup).Assembly);
                ef.AddAspNetCore();
                ef.UseConsoleLog();
                ef.RegisterModule<DomainModule>();
                ef.RegisterModule<TransactionReadModelModule>();
                ef.RegisterServices(x=> 
                {
                    x.Register<ITransactionCommandService, TransactionCommandService>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounting API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
