using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.History.Authentication.Settings;

namespace Service.History.Authentication.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationOptions
    {
        public static IServiceCollection AddGoogleAdSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleAd>(options => configuration.GetSection("GoogleAd").Bind(options));

            return services;
        }
    }
}
