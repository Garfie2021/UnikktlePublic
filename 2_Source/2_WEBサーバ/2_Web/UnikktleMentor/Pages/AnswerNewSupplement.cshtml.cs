using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using UnikktleMentor.Cache;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using UnikktleMentor.Common;
using UnikktleMentorEngine;
using UnikktleCommon;


namespace UnikktleMentor.Pages.Home
{
    public class AnswerNewSupplement : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AnswerNewSupplement(
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

        [BindProperty]
        public InputModel_Answer_Supplement _Input { get; set; }

        public string StatusMessage { get; set; }

        //public MindViewModel MindViewModel { get; set; }

        public string PageTitle;
        
        public bool View_AllowOtherEdit { get; set; } = false;

        public List<CareerCategory> _CareerList { get; set; }

        public UserSetting _UserSettingProfile = new UserSetting();

        public async Task<IActionResult> OnGet()
        {
            try
            {
                // 未ログインで診断のみするケースがある。
                // ログインしている場合、ユーザープロファイルを取得する。
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (!bad)
                {
                    var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                    if (no != null)
                    {
                        var userNo = (long)no;

                        _UserSettingProfile = SessionCache.GetUserSettingProfile(
                            HttpContext.Session, _dbContext, _Cache, _userManager, _emailSender, User);
                    }
                }

                SetValue();

                _Input._Gender = _UserSettingProfile.Gender;
                _Input.Career = _UserSettingProfile.Career;

                StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage);
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public IActionResult OnPostBackAsync()
        {
            try
            {
                // 未ログインで診断のみするケースがある。
                //(bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                //if (bad)
                //{
                //    return LocalRedirect("/Identity/Account/Login");
                //}

                //var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                //if (no == null)
                //{
                //    return BadRequest();
                //}
                //var userNo = (long)no;

                var currentPage = (int)HttpContext.Session.GetInt32(SessionKey.CurrentPage) - 1;
                HttpContext.Session.SetInt32(SessionKey.CurrentPage, currentPage);

                return LocalRedirect("/AnswerNew");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public IActionResult OnPostConfirmAsync()
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}
                if (!Validate())
                {
                    // Error. 未選択の項目があります。
                    HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["29"]);
                    SetValue();
                    return Page();
                }

                // 未ログインで診断のみするケースがある。
                //(bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                //if (bad)
                //{
                //    return LocalRedirect("/Identity/Account/Login");
                //}

                //var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                //if (no == null)
                //{
                //    return BadRequest();
                //}
                //var userNo = (long)no;

                HttpContext.Session.Set(SessionKey.AnswerSupplement, _Input);

                return LocalRedirect("/AnswerNewConfirm");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        private void SetValue()
        {
            // 回答
            _Input = SessionValue.GetAnswerSupplement(HttpContext.Session);

            _CareerList = InMemoryCache.GetCareerAndCategoryList(_dbContext, _Cache);

//#if DEBUG
//            _Input.RecentHappenings = "いろいろ有った。";
//#endif
        }

        private bool Validate()
        {
            //if (_Input.A0 == null || _Input.A1 == null || _Input.A2 == null || _Input.A3 == null || _Input.A4 == null || _Input.A5 == null || _Input.A6 == null || _Input.A7 == null || _Input.A8 == null || _Input.A9 == null ||
            //    _Input.A10 == null || _Input.A11 == null || _Input.A12 == null || _Input.A13 == null || _Input.A14 == null || _Input.A15 == null || _Input.A16 == null || _Input.A17 == null || _Input.A18 == null || _Input.A19 == null)
            //{
            //    // 未入力
            //    return false;
            //}

            return true;
        }

    }
}