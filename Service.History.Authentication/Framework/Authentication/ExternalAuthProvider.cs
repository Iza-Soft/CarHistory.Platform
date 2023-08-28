using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.History.Authentication.Pattern.Strategy.Class;
using Service.History.Authentication.Pattern.Strategy.Interface;

namespace Service.History.Authentication.Framework.Authentication
{
    public class ExternalAuthProvider
    {
        private IAuthProvider _authProvider = null!;
        private readonly IServiceProvider _serviceProvider = null!;

        public ExternalAuthProvider(IServiceProvider provider)
        {
            this._serviceProvider = provider;
        }

        // Usually, the AuthProvider accepts a strategy through the constructor, but
        // also provides a setter to change it at runtime.
        public ExternalAuthProvider(IAuthProvider authProvider)
        {
            this._authProvider = authProvider;
        }

        // Usually, the AuthProvider allows replacing a Strategy object at runtime.
        public void SetProvider(string provider)
        {
            this._authProvider = provider switch
            {
                GoogleDefaults.AuthenticationScheme => new GoogleProvider(this._serviceProvider),
                MicrosoftAccountDefaults.AuthenticationScheme => new MicrosoftProvider(this._serviceProvider),
                _ => throw new NotImplementedException()
            };
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
    }
}
