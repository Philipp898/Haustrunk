using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Haustrunk.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}