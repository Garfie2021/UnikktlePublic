using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Unikktle.Cache;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using UnikktleCommon;

namespace Unikktle.Controllers
{
    // 関連ワード検索WebAPI 一般画面用
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;

        public SearchController(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMemoryCache cache,
            IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _Cache = cache;
            _emailSender = emailSender;
        }

        // GET: https://localhost:44376/Search?SearchString={SearchString}?r={r}    
        // 例 https://localhost:44376/Search?SearchString=c%23&r=0
        // s：検索文字列
        // p：ページ数 兼 PG行数
        [HttpGet(Name = "GetSearch")]
        public async Task<SearchViewModel> Get(string s, int? p)
        {
            try
            {
                if (string.IsNullOrEmpty(s) || p == null)
                {
                    //return new SearchViewModel()
                    //{
                    //    KeywordList = new List<KeywordSearch>(),
                    //    NextAvailable = false,
                    //};
                    return null;
                }

                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                var viewModel = InMemoryCache.Search(_dbContext, _Cache, s, (int)p);

                viewModel.AdverSelectPrSearch = ControlCommon.GetSearchPR(_dbContext,
                    s, userNo,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    (int)p + 1);

                return viewModel;
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "s: " + s + "<br>r: " + p, ex);
            }

            return new SearchViewModel();
        }

    }
}
