using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.History.Core.Pattern.Proxy.Interface;

namespace Service.History.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IProxyPages _pages;
        private readonly IProxyAuthProvider _proxyAuthProvider;

        public AccountController(IProxyAuthProvider proxyAuthProvider, IProxyPages pages)
        {
            this._pages = pages;
            this._proxyAuthProvider = proxyAuthProvider;
        }

        [HttpGet]
        [ActionName("login")]
        public IActionResult Login()
        {
            this._proxyAuthProvider.SetProvider(GoogleDefaults.AuthenticationScheme);

            return this._proxyAuthProvider.ChallengeResult();
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse(string returnUrl = null!)
        {

            var result = await this._proxyAuthProvider.ExternalLoginSignInAsync();

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), this._pages.Controller<HomeController>());
            }

            IdentityResult identityResult = await this._proxyAuthProvider.CreateExternalLoginAsync();

            if (identityResult.Succeeded)
            {
                await this._proxyAuthProvider.SignInAsync();

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), this._pages.Controller<HomeController>());

            }

            return RedirectToAction("AccessDenied", this._pages.Controller<AccountController>());
        }

        [HttpGet]
        public IActionResult MicrosoftResponse(string returnUrl = null!)
        {
            throw new NotImplementedException();
        }
    }
}
