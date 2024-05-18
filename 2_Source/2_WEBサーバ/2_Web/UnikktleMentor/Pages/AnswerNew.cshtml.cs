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
    public class AnswerNew : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AnswerNew(
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
        public InputModel_AnswerEdit _Input { get; set; }

        public string StatusMessage { get; set; }

        //public MindViewModel MindViewModel { get; set; }

        public string PageTitle;

        //public QuestionViewModel _ViewModel;
        public List<Question> _QuestionList { get; set; }

        public int _CurrentPage { get; set; }
        
        public bool View_AllowOtherEdit { get; set; } = false;

        public DateTime _AnswerNewStart;


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // 未ログインで診断のみするケースがある。

                var currentPage = HttpContext.Session.GetInt32(SessionKey.CurrentPage);
                if (currentPage == null)
                {
                    // 未ログインTopページか、ログイン済みHomeページの診断開始ボタンをクリックして、
                    // 遷移してきた場合、nullになることはない。
                    throw new Exception("ありえない");
                }

                StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage);
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                if (currentPage < 1)
                {

                    (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                    if (bad)
                    {
                        // 未ログイン

                        // 最初のページだけ回数チェックする。
                        var answerHistory = SP_AnswerHistoryNoLogin.Select(_dbContext, HttpContext.Session.Id);
                        if (answerHistory.Count() > 0)
                        {
                            if (answerHistory[0].AnswerDate > DateTime.Now.AddMonths(-1))
                            {
                                // 診断できるのは1ヵ月に1回までです。\r\n最後に診断した日時：
                                HttpContext.Session.SetString(SessionKey.StatusMessage,
                                    _sharedLocalizer["26"] + answerHistory[0].AnswerDate.ToString());

                                //return LocalRedirect("/Home/Status");
                                return LocalRedirect("/Home/Index");
                            }
                        }
                    }
                    else
                    {
                        var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                        if (no == null)
                        {
                            return BadRequest();
                        }
                        var userNo = (long)no;

                        var answerHistory = SP_AnswerHistory.Select_Desc(_dbContext, userNo);
                        if (answerHistory.Count() > 0)
                        {
                            if (answerHistory[0].AnswerDate > DateTime.Now.AddMonths(-1))
                            {
                                // 診断できるのは1ヵ月に1回までです。
                                HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["27"]);
                                return LocalRedirect("/");
                            }
                        }
                    }
                }

                SetValue();

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public IActionResult OnPostNextAsync()
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    SetValue();
                //    return Page();
                //}
                if (!Validate())
                {
                    // Error. 未入力の項目があります。
                    StatusMessage = _sharedLocalizer["28"];
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

                // 現在ページ
                _CurrentPage = (int)HttpContext.Session.GetInt32(SessionKey.CurrentPage);

                HttpContext.Session.SetInt32(SessionKey.CurrentPage, _CurrentPage);

                // 120項目設問の回答
                SessionValue.SetQuestionAnswerList(HttpContext.Session, _CurrentPage, _Input);

                _CurrentPage += 1;
                HttpContext.Session.SetInt32(SessionKey.CurrentPage, _CurrentPage);

                if (_CurrentPage < 6)
                {
                    return LocalRedirect("/AnswerNew");
                }
                else
                {
                    return LocalRedirect("/AnswerNewSupplement");
                }

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
                if (!ModelState.IsValid)
                {
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
            // 現在ページ
            _CurrentPage = (int)HttpContext.Session.GetInt32(SessionKey.CurrentPage);

            // 設問
            _QuestionList = MemoryCacheValue.GetQuestionList(_Cache, _CurrentPage);

            // 回答
            _Input = SessionValue.GetAnswerList(HttpContext.Session)[_CurrentPage];

            _AnswerNewStart = HttpContext.Session.Get<DateTime>(SessionKey.AnswerNewStart);
        }

        private bool Validate()
        {
            if (_Input.A0 == null || _Input.A1 == null || _Input.A2 == null || _Input.A3 == null || _Input.A4 == null || _Input.A5 == null || _Input.A6 == null || _Input.A7 == null || _Input.A8 == null || _Input.A9 == null ||
                _Input.A10 == null || _Input.A11 == null || _Input.A12 == null || _Input.A13 == null || _Input.A14 == null || _Input.A15 == null || _Input.A16 == null || _Input.A17 == null || _Input.A18 == null || _Input.A19 == null)
            {
                // 未入力
                return false;
            }

            return true;
        }

    }
}