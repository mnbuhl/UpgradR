using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Behaviors;
using Order.Application.Orders.v1;
using System.Reflection;

namespace Order.Application
{
    public static class ApplicationServiceExtensions
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
