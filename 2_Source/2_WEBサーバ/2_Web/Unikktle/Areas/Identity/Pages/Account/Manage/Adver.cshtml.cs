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
    public partial class AdverModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AdverModel(
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

        //[BindProperty]
        //public InputModel_Adver Input { get; set; }

        public List<AdverSearchSelect> AdverSearchList { get; set; }
        public List<AdverRelationSelect> AdverRelationList { get; set; }

        public short BusinessId { get; set; }

        // 組織表示用
        public string BusinessCategory { get; set; }
        public string OrganizationName { get; set; }

        // idb: BusinessId
        public async Task<IActionResult> OnGetAsync(short? idb)
        {
            try
            {
                if (idb == null)
                {
                    return BadRequest();
                }

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                var businessNo = (short)idb;
                HttpContext.Session.SetInt32(SessionKey.BusinessId, businessNo);

                var business = SP_Business.SelectOne(_dbContext, userNo, businessNo);
                BusinessCategory = business.Category.ToString();
                OrganizationName = business.OrganizationName;

                AdverSearchList = SP_AdverSearch.Select(_dbContext, userNo, businessNo);
                AdverRelationList = SP_AdverRelation.Select(_dbContext, userNo, businessNo);

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "idb: " + idb, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAddSearchAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.新規登録);

                return LocalRedirect("/Identity/Account/Manage/AdverSearchEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAddRelationAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.新規登録);

                return LocalRedirect("/Identity/Account/Manage/AdverRelationEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostEditSearchAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.編集開始);

                return LocalRedirect("/Identity/Account/Manage/AdverSearchEdit?id=" + id);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostEditRelationAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.編集開始);

                return LocalRedirect("/Identity/Account/Manage/AdverRelationEdit?id=" + id);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        // id: AdverId
        public async Task<IActionResult> OnPostDeleteSearchAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var businessNo = (short)HttpContext.Session.GetInt32(SessionKey.BusinessId);

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                SP_AdverSearch.Delete(_dbContext, userNo, businessNo, (int)id);

                StatusMessage = _sharedLocalizer["12"]; // "削除が完了しました";

                return LocalRedirect("/Identity/Account/Manage/Adver?idb=" + businessNo);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        // id: AdverId
        public async Task<IActionResult> OnPostDeleteRelationAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var businessNo = (short)HttpContext.Session.GetInt32(SessionKey.BusinessId);

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                SP_AdverRelation.Delete(_dbContext, userNo, businessNo, (int)id);

                StatusMessage = _sharedLocalizer["12"]; // "削除が完了しました";

                return LocalRedirect("/Identity/Account/Manage/Adver?idb=" + businessNo);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
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

            return BadRequest();
        }

    }
}
