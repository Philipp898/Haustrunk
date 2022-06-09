using FluentValidation.AspNetCore;
using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Backend.Filter;
using Haustrunk.Backend.Services;

namespace Haustrunk.Backend
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            return services;
        }
    }
}
