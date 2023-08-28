using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.History.DataAccess.Authentication;

namespace Service.History.DataAccess.Extensions.Service
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDdManagement(this IServiceCollection services, IConfiguration configuration)
        {
            /*
             * use power shell for migration with commands 
             ********************************************** if have some problem with command "dotnet ef", see link: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli **********************************************
             *      1. Test dotnet CLI with command dotnet ef
             *      2. if there is problem whit dotnet CLI
             *      3. Run command in Developer Command Prompt or Developer PowerShell: dotnet tool install --global dotnet-ef 
             *      4. Install the latest Microsoft.EntityFrameworkCore.Design package.
             *************************************************************************************************************************************************************************************************************************************************
             * dotnet ef migrations add InitializeDataBase --startup-project Service.History.Web --project Service.History.DataAccess
             * dotnet ef database update --startup-project Service.History.Web --project Service.History.DataAccess
             * 
             * see link: https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx
             * 
             * if have been used CLI for DB migration, that what you have to do is to add migration assembly (place that have been used for the migration)
             * Here, can use Lazy Loading Related Data, as adding UseLazyLoadingProxies(), but for this purpose have to install Microsoft.EntityFrameworkCore.Proxies package
             * in Package Manager: Install-Package Microsoft.EntityFrameworkCore.Proxies -Version 2.2.0
             */
            /*https://andrewlock.net/implementing-custom-token-providers-for-passwordless-authentication-in-asp-net-core-identity/*/
            /*https://www.dotnettricks.com/learn/aspnetcore/authentication-authentication-aspnet-identity-example*/
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole<Guid>>().AddUserManager<UserManager<ApplicationUser>>().AddEntityFrameworkStores<ServiceHistoryContext>(); //need to add -> AddTokenProvider<AuthenticatorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            services.AddDbContext<ServiceHistoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
