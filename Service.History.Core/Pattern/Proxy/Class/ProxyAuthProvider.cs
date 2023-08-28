using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.History.Core.Pattern.Proxy.Interface;
using Service.History.Core.Framework.Authentication.Class;

namespace Service.History.Core.Pattern.Proxy.Class
{
    public class ProxyAuthProvider : IProxyAuthProvider
    {
        #region Properties
        private readonly AuthProvider _authProvider;
        #endregion

        #region Constructor
        public ProxyAuthProvider(IServiceProvider provider)
        {
            this._authProvider = new AuthProvider(provider);
        }
        #endregion

        #region Public methods

        public void SetProvider(string provider)
        {
            this._authProvider.SetProvider(provider);
        }

        public ChallengeResult ChallengeResult()
        {
            return this._authProvider.ChallengeResult();
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync()
        {
            return await this._authProvider.ExternalLoginSignInAsync();
        }

        public async Task<IdentityResult> CreateExternalLoginAsync()
        {
            return await this._authProvider.CreateExternalLoginAsync();
        }

        public async Task SignInAsync()
        {
            await this._authProvider.SignInAsync();
        }

        #endregion
    }
}
