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
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Unikktle.Cache;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Web;
using Microsoft.Extensions.Localization;
using UnikktleCommon;

namespace Unikktle.Controllers
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

                if (string.IsNullOrEmpty(model.StatusMessage))
                {
                    return LocalRedirect("/");   // トップページに遷移
                }

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
        // GET: https://localhost:44376/Home/
        public IActionResult Index()
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
        // GET: https://localhost:44376/Home/About
        public IActionResult About()
        {
            try
            {
                return View(new AboutViewModel()
                {
                    KeywordCount = InMemoryCache.KeywordCount(_dbContext, _Cache)
                });
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
        public async Task<IActionResult> Search(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    return LocalRedirect("/");   // トップページに遷移
                }

                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                var viewModel = InMemoryCache.Search(_dbContext, _Cache, s, 0);

                viewModel.AdverSelectPrSearch = ControlCommon.GetSearchPR(_dbContext,
                    s, userNo,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    1);

                if (viewModel.KeywordList.Count() < 1)
                {
                    // 外部検索
                    viewModel.ExternalSearch = ControlCommon.GetExternalSearchURL(
                        SessionCache.GetExternalSearchEngine(
                            HttpContext.Session, _dbContext, _Cache, _emailSender,
                            userNo), s);
                }

                HttpContext.Session.SetString(SessionKey.UserSearchString, s);
                HttpContext.Session.SetString(SessionKey.HeaderTitile, _sharedLocalizer["112", s]);

                viewModel.MindSearchViewModel = ControlCommonMind.Search(_dbContext, s, 0, userNo);

                if (viewModel.MindSearchViewModel.MindList.Count() < 1)
                {
                    // 外部検索
                    viewModel.MindSearchViewModel.ExternalSearch = ControlCommon.GetExternalSearchURL(
                        SessionCache.GetExternalSearchEngine(
                            HttpContext.Session, _dbContext, _Cache, _emailSender,
                            userNo), s);
                }

                //#if DEBUG
                //                // テストデータ
                //                var mindList = new List<MindSearch>();
                //                mindList.Add(new MindSearch() { Id = 1, Title = "タイトル１" });
                //                mindList.Add(new MindSearch() { Id = 2, Title = "タイトル２" });

                //                viewModel.MindSearchViewModel = new MindSearchViewModel()
                //                {
                //                    MindList = mindList,
                //                    NextAvailable = ControlCommon.NextAvailable(1, 1)
                //                };
                //#endif

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                // エラーが多過ぎるので、当面は無視する
                MailNotify.ExceptionMail(_emailSender, "s: " + s, ex);
            }

            return BadRequest();
        }

        // GET: https://localhost:44376/Home/WordMap/1?pageNum=2
        // 変数名は「No」禁止。「Id」じゃないとnullになる。
        // i：id
        public async Task<IActionResult> WordMap(long? i)
        {
            try
            {
                if (i == null)
                {
                    return LocalRedirect("/");   // トップページに遷移
                }

                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                // クリック履歴
                if (SessionCache.IsExistHistory(HttpContext.Session, (long)i) == false)
                {
                    SP_RelationWordClickHistory.Insert(_dbContext, (long)i, userNo,
                        HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                }

                // 関連ワード
                var viewModel = InMemoryCache.GetWordMapViewModel(_dbContext, _Cache, (long)i, 0);

                // 広告
                viewModel.SvgWordMap.AdverSelectPrRelation = ControlCommon.GetRelationPR(_dbContext,
                    (long)i, userNo,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    1);

                ControlCommon.表示位置領域計算(viewModel);

                // 外部検索
                viewModel.ExternalSearch = ControlCommon.GetExternalSearchURL(
                    SessionCache.GetExternalSearchEngine(
                        HttpContext.Session, _dbContext, _Cache, _emailSender,
                        userNo), "");

                HttpContext.Session.SetString(SessionKey.UserRelationString, viewModel.Word);
                HttpContext.Session.SetString(SessionKey.HeaderTitile, _sharedLocalizer["114", viewModel.Word]);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "i:" + i, ex);
            }

            return BadRequest();
        }

        // プライバシー
        public IActionResult Privacy()
        {
            try
            {
                return View(new PrivacyViewModel());
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
