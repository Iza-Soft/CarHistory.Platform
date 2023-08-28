using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Service.History.Authentication.Pattern.Strategy.Interface;
using Service.History.DataAccess.Authentication;

namespace Service.History.Authentication.Pattern.Strategy.Class
{
    internal class MicrosoftProvider : IAuthProvider
    {
        #region Properties

        private readonly IServiceProvider _provider;
        private ExternalLoginInfo _info = null!;
        private ApplicationUser _user = null!;

        #endregion

        #region Constructor

        public MicrosoftProvider(IServiceProvider provider)
        {
            this._provider = provider;
        }

        #endregion

        #region Public methods

        public ChallengeResult ChallengeResult()
        {
            using (IServiceScope scope = this._provider.CreateScope())
            {
                SignInManager<ApplicationUser> signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                var properties = signInManager.ConfigureExternalAuthenticationProperties(MicrosoftAccountDefaults.AuthenticationScheme, this.MicrosoftResponseUrl());

                return new ChallengeResult(MicrosoftAccountDefaults.AuthenticationScheme, properties);
            }
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync()
        {
            using (IServiceScope scope = this._provider.CreateScope())
            {
                SignInManager<ApplicationUser> signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                this._info = await this.GetUserInfoAsync();

                return await signInManager.ExternalLoginSignInAsync(this._info.LoginProvider, this._info.ProviderKey, false);
            }
        }

        public async Task<IdentityResult> CreateExternalLoginAsync()
        {
            using (IServiceScope scope = this._provider.CreateScope())
            {
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                this.CreateUser();

                IdentityResult identResult = await userManager.CreateAsync(this._user);

                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(this._user, this._info);
                }

                return identResult;
            }
        }

        public async Task SignInAsync()
        {
            using (IServiceScope scope = this._provider.CreateScope())
            {
                SignInManager<ApplicationUser> signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                await signInManager.SignInAsync(this._user, false);
            }
        }

        #endregion

        #region Private methods

        private void CreateUser() => this._user = new ApplicationUser()
        {
            Forename = this._info.Principal.FindFirst(ClaimTypes.GivenName)!.Value,
            Surname = this._info.Principal.FindFirst(ClaimTypes.Surname)!.Value,
            Email = this._info.Principal.FindFirst(ClaimTypes.Email)!.Value,
            UserName = this._info.Principal.FindFirst(ClaimTypes.Email)!.Value
        };

        private string MicrosoftResponseUrl()
        {
            using (IServiceScope scope = this._provider.CreateScope())
            {
                IActionContextAccessor actionContextAccessor = scope.ServiceProvider.GetRequiredService<IActionContextAccessor>();

                IUrlHelperFactory urlHelperFactory = scope.ServiceProvider.GetRequiredService<IUrlHelperFactory>();

                IUrlHelper url = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);

                return url.Action("MicrosoftResponse", "Account")!;
            }
        }

        private async Task<ExternalLoginInfo> GetUserInfoAsync()
        {
            using (IServiceScope scope = this._provider.CreateScope())
            {
                SignInManager<ApplicationUser> signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                return await signInManager.GetExternalLoginInfoAsync();
            }
        }

        #endregion
    }
}
