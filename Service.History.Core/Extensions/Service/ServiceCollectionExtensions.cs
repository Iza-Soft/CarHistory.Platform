using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Service.History.Core.Pattern.Proxy.Class;
using Service.History.Core.Pattern.Proxy.Interface;

namespace Service.History.Core.Extensions.Service
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProxyProvider(this IServiceCollection services)
        {
            services.AddTransient<IProxyAuthProvider, ProxyAuthProvider>();
            services.AddScoped<IProxyPages, ProxyPages>();

            return services;
        }
    }
}
