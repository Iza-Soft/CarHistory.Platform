using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Service.History.Authentication.Framework.Authentication;
using Service.History.Authentication.Settings;

namespace Service.History.Authentication.Extensions.Service
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalAuthentication(this IServiceCollection services)
        {
            IOptions<GoogleAd>? iop = services.BuildServiceProvider().GetService<IOptions<GoogleAd>>();

            services.AddAuthentication(IdentityConstants.ExternalScheme)
                .AddCookie(IdentityConstants.ExternalScheme)
                .AddCookie(IdentityConstants.ApplicationScheme)
                .AddGoogle(opts =>
                {
                    opts.ClientId = iop!.Value.ClientId;
                    opts.ClientSecret = iop!.Value.ClientSecret;
                    opts.SignInScheme = IdentityConstants.ExternalScheme;
                });

            return services;
        }

        public static IServiceCollection AddAuthProvider(this IServiceCollection services)
        {
            services.AddSingleton<ExternalAuthProvider>();

            return services;
        }
    }
}
