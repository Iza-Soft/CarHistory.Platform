using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Service.History.Authentication.Framework.Authentication;
using Service.History.Core.Pattern.Proxy.Interface;

namespace Service.History.Core.Framework.Authentication.Class
{
    internal class AuthProvider : IProxyAuthProvider
    {
        #region Properties
        private readonly ExternalAuthProvider _externalAuthProvider;
        #endregion

        #region Constructor
        public AuthProvider(IServiceProvider provider)
        {
            using (IServiceScope scope = provider.CreateScope())
            {
                this._externalAuthProvider = scope.ServiceProvider.GetRequiredService<ExternalAuthProvider>();
            }
        }

        #endregion

        #region Public methods

        public void SetProvider(string provider)
        {
            this._externalAuthProvider.SetProvider(provider);
        }

        public ChallengeResult ChallengeResult()
        {
            return this._externalAuthProvider.ChallengeResult();
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync()
        {
            return await this._externalAuthProvider.ExternalLoginSignInAsync();
        }


        public async Task<IdentityResult> CreateExternalLoginAsync()
        {
            return await this._externalAuthProvider.CreateExternalLoginAsync();
        }

        public async Task SignInAsync()
        {
            await this._externalAuthProvider.SignInAsync();
        }

        #endregion

    }
}
