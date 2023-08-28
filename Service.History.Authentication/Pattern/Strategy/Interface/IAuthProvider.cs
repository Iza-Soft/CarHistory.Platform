using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Service.History.Authentication.Pattern.Strategy.Interface;

public interface IAuthProvider
{
    ChallengeResult ChallengeResult();

    Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync();

    Task<IdentityResult> CreateExternalLoginAsync();

    Task SignInAsync();
}