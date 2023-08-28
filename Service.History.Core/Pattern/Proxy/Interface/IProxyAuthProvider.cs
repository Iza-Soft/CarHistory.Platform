using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Service.History.Core.Pattern.Proxy.Interface;

public interface IProxyAuthProvider
{
    void SetProvider(string provider);

    ChallengeResult ChallengeResult();

    Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync();

    Task<IdentityResult> CreateExternalLoginAsync();

    Task SignInAsync();
}