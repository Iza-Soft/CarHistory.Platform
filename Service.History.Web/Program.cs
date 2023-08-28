using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Serialization;
using Service.History.Authentication.Configuration;
using Service.History.Authentication.Extensions.Service;
using Service.History.Core.Extensions.Service;
using Service.History.DataAccess.Authentication;
using Service.History.DataAccess.Extensions.Service;
using Service.History.Infrastructure.Extensions.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region Add Configuration Services

builder.Services.AddGoogleAdSettings(configuration);

#endregion

#region Add services to the container

builder.Services.AddHealthChecks();

builder.Services.AddControllersWithViews();

builder.Services.AddDdManagement(configuration);

builder.Services.AddExternalAuthentication();

builder.Services.AddKendo();

builder.Services.AddRazorPages().AddViewOptions(options => { options.HtmlHelperOptions.ClientValidationEnabled = true; }) /*Enable client side validation or disable it: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1#disable-client-side-validation */
                .AddRazorRuntimeCompilation() /*To enable runtime compilation for all environments and configuration modes: https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-compilation?view=aspnetcore-6.0&tabs=visual-studio */
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<SignInManager<ApplicationUser>>();

builder.Services.AddMapperProvider();
builder.Services.AddAuthProvider();
builder.Services.AddProxyProvider();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
}).RequireAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        "areas",
//        "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

//    endpoints.MapRazorPages();

//    endpoints.MapDefaultControllerRoute();

//});

app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapDefaultControllerRoute();

app.Run();
