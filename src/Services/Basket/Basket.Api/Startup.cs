using Basket.Core.Contracts.v1.ShoppingCarts;
using Basket.Core.Interfaces;
using Basket.Infrastructure.GrpcServices;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos.v1;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Basket.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = _configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();

            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
                opt.Address = new Uri(_configuration.GetValue<string>("GrpcSettings:DiscountUrl")));

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((_, cfg) =>
                {
                    cfg.Host(_configuration.GetValue<string>("EventBusSettings:HostAddress"));
                });
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.RouteConstraintName = "apiVersion";
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
