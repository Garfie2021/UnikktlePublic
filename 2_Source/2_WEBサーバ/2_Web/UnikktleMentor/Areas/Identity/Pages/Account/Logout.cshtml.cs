using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using UnikktleMentor.Common;
using UnikktleCommon;

namespace UnikktleMentor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public LogoutModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }


        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            try
            {
                (bool bad, IdentityUser userId) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                await _signInManager.SignOutAsync();

                HttpContext.Session.Clear();

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Page();
                }
                else
                {
                    return LocalRedirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "returnUrl: " + returnUrl, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "ÉGÉâÅ[Ç™î≠ê∂ÇµÇ‹ÇµÇΩÅB"
            }

            return BadRequest();
        }
    }
}