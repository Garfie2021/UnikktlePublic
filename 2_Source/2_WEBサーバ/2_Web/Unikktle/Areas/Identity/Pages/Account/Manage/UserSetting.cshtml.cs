using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Unikktle.Cache;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using UnikktleCommon;

namespace Unikktle.Areas.Identity.Pages.Account.Manage
{
    public partial class UserSettingModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UserSettingModel(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMemoryCache cache,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _Cache = cache;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel_UserSetting Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    MailNotify.DebugMail(_emailSender, "OnGetAsync() 1. LocalRedirect(\"/Identity/Account/Login\").");

                    return LocalRedirect("/Identity/Account/Login");
                }

                var userSetting = SessionCache.GetUserSetting(
                    HttpContext.Session, _dbContext, _Cache, _emailSender, _userManager, User);

                Input = new InputModel_UserSetting
                {
                    BackgroundColor = userSetting.BackgroundColor,
                    ExternalSearchEngine = userSetting.ExternalSearchEngine
                };

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            MailNotify.DebugMail(_emailSender, "OnGetAsync() 2. BadRequest().");

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    MailNotify.DebugMail(_emailSender, "OnPostAsync() 1. Page().");

                    return Page();
                }

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    MailNotify.DebugMail(_emailSender, "OnPostAsync() 2. LocalRedirect(\"/Identity/Account/Login\").");

                    return LocalRedirect("/Identity/Account/Login");
                }

                HttpContext.Session.SetInt32(SessionKey.BackgroundColor, (int)Input.BackgroundColor);
                HttpContext.Session.SetInt32(SessionKey.ExternalSearchEngine, (int)Input.ExternalSearchEngine);

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    MailNotify.DebugMail(_emailSender, "OnPostAsync() 3. Page().");

                    return BadRequest();
                }
                var userNo = (long)no;

                SP_UserSetting.Update(_dbContext, userNo,
                    Input.BackgroundColor, Input.ExternalSearchEngine);

                StatusMessage = _sharedLocalizer["15"]; // "Your user setting has been updated"

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            MailNotify.DebugMail(_emailSender, "OnPostAsync() 4. Page().");

            return BadRequest();
        }
    }
}
