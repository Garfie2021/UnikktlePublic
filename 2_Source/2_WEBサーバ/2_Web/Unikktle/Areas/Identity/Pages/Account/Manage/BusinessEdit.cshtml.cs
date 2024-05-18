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
    public partial class BusinessEditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public BusinessEditModel(
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
        public InputModel_Business Input { get; set; }

        public Business Business { get; set; }

        // id：BusinessId
        public async Task<IActionResult> OnGetAsync(short? id)
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                if (id == null)
                {
                    HttpContext.Session.SetString(SessionKey.BusinessEditModel_No, "");
                    return Page();
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                Business = SP_Business.SelectOne(_dbContext, userNo, (short)id);

                Input = new InputModel_Business()
                {
                    Category = Business.Category,
                    OrganizationName = Business.OrganizationName,
                    OrganizationURL = Business.OrganizationURL
                };

                HttpContext.Session.SetString(SessionKey.BusinessEditModel_No, id.ToString());
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no2 = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no2 == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no2;

                var no = HttpContext.Session.GetString(SessionKey.BusinessEditModel_No);

                if (string.IsNullOrEmpty(no))
                {
                    SP_Business.Insert(_dbContext, userNo, Input.Category,
                        Input.OrganizationName, Input.OrganizationURL);

                    //StatusMessage = "登録が完了しました";
                    StatusMessage = _sharedLocalizer["10"];
                }
                else
                {
                    SP_Business.Update(_dbContext, userNo, short.Parse(no), Input.Category,
                        Input.OrganizationName, Input.OrganizationURL);

                    // "編集が完了しました";
                    StatusMessage = _sharedLocalizer["11"];
                }

                return LocalRedirect("/Identity/Account/Manage/Business");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return Page();
        }

        public IActionResult OnPostBack()
        {
            try
            {
                return LocalRedirect("/Identity/Account/Manage/Business");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return Page();
        }

    }
}
