using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Common;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using UnikktleMentor.Cache;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Web;
using Microsoft.Extensions.Localization;
using UnikktleCommon;

namespace UnikktleMentor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(
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

        // 規定メソッド
        // 
        // GET: https://localhost:44376/Home/Status
        public IActionResult Status()
        {
            try
            {
                var model = new StatusViewModel()
                {
                    StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage)
                };
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                if (string.IsNullOrEmpty(model.StatusMessage))
                {
                    return LocalRedirect("/");   // トップページに遷移
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }


        // 規定メソッド
        // 
        // GET: https://localhost:44376/Home/
        public async Task<IActionResult> Index()
        {
            try
            {
                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                if (userNo > -1)
                {
                    // ログイン済みならHome画面を表示する
                    return LocalRedirect("/Home");
                }

                var model = new IndexViewModel()
                {
                    StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage)
                };
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }


        // 規定メソッド
        // 
        // GET: https://localhost:44376/Home/About
        public IActionResult About()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        // 規定メソッド
        // 
        // GET: https://localhost:44376/Home/Search
        // s：検索文字列
        [HttpPost]
        public IActionResult AnswerNewStart()
        {
            try
            {
                var answerHistory = SP_AnswerHistoryNoLogin.Select(_dbContext, HttpContext.Session.Id);
                if (answerHistory.Count() > 0)
                {
                    if (answerHistory[0].AnswerDate > DateTime.Now.AddMonths(-1))
                    {
                        HttpContext.Session.SetString(SessionKey.StatusMessage,
                            "診断できるのは1ヵ月に1回までです。\r\n最後に診断した日時："
                                + answerHistory[0].AnswerDate.ToString());

                        //return LocalRedirect("/Home/Status");
                        return LocalRedirect("/Home/Index");
                    }
                }

                ControlCommon.InitalizeSession_Answer(HttpContext.Session);

                return LocalRedirect("/AnswerNew");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "", ex);
            }

            return BadRequest();
        }


        // プライバシー
        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            try
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "culture: " + culture + "<br>returnUrl: " + returnUrl, ex);
            }

            return BadRequest();
        }

    }
}
