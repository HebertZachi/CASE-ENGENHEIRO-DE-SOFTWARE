using Adapters.ViaCep;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters
{
    public static class AdaptersDependencyInjection
    {
        public static IServiceCollection AddAdapters(this IServiceCollection services)
        {
            services.AddScoped<IViaCepService, ViaCepService>();

            return services;
        }
    }
}
