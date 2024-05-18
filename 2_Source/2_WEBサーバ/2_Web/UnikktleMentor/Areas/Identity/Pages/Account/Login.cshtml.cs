using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using UnikktleMentor.Common;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using UnikktleCommon;

namespace UnikktleMentor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public LoginModel(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }


        [BindProperty]
        public InputModel_Login Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                }

                returnUrl = returnUrl ?? Url.Content("/");

                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                ReturnUrl = returnUrl;

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "returnUrl: " + returnUrl, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //returnUrl = returnUrl ?? Url.Content("/");
            try
            {
                //returnUrl = returnUrl ?? Url.Content("~/");

                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Input.UserID, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        var userNo = SP_AspNetUsers.Select_FromUserName(_dbContext, Input.UserID);
                        //var userNo = SP_AspNetUsers.AspNetUsers_Select_FromId(_dbContext, _userManager.GetUserId(User));
                        SP_UserSetting.UpdateIPv4(_dbContext, userNo[0].No,
                            HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                        //if (!string.IsNullOrEmpty(Input.Latitude) && !string.IsNullOrEmpty(Input.Longitude))
                        //{
                        //    SP_UserSetting.UserSetting_UpdateGeolocation(_dbContext, userNo[0].No,
                        //        double.Parse(Input.Latitude), double.Parse(Input.Longitude));
                        //}

                        HttpContext.Session.Clear();

                        //return LocalRedirect(returnUrl);
                        return LocalRedirect("/Home");
                    }

                    //if (result.RequiresTwoFactor)
                    //{
                    //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    //}

                    if (result.IsLockedOut)
                    {
                        // "ログイン失敗が連続した為、アカウントはロックされました。暫く経ってからログインして下さい。"
                        ModelState.AddModelError(string.Empty, _sharedLocalizer["24"]);
                        return Page();
                    }
                    else
                    {
                        // "Invalid login attempt."
                        ModelState.AddModelError(string.Empty, _sharedLocalizer["25"]);
                        return Page();
                    }
                }

                // If we got this far, something failed, redisplay form

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "returnUrl: " + returnUrl, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }
    }
}
