/* フェーズ2で使う
 */
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
    [Route("[controller]")]
    [ApiController]
    public class ClickPrSearchController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;

        public ClickPrSearchController(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            IMemoryCache cache,
            IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _Cache = cache;
            _emailSender = emailSender;
        }

        // GET: https://localhost:44376/PRClick?SearchString={SearchString}?pageNum={pageNum}    
        // 例 https://localhost:44376/PRClick?SearchString=c%23&pageNum=0
        //
        // u: uid: UserNo 広告主のUser
        // a: aid: AdverNo
        // b: bid: BusinessNo
        // w: wid: WordNo
        [HttpGet(Name = "GetClickPrSearch")]
        public async Task Get(long? u, short? b, int? a, long? w)
        {
            try
            {
                if (u == null || b == null || a == null || w == null)
                {
                    return;
                }

                if (SessionCache.IsExistHistory(HttpContext.Session,
                    (long)u, (short)b, (int)a, (long)w))
                {
                    // 既にクリックされた。
                    return;
                }

                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                SP_AdverSearchClickHistory.Insert(_dbContext,
                    (long)u, (short)b, (int)a, (long)w,
                    userNo, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    out string email);

                if (!string.IsNullOrEmpty(email))
                {
                    await _emailSender.SendEmailAsync(email,
                        "[Unikktle] 0 Credit",
                        "保有Creditが0以下になりました。");
                }
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "uid: " + u + "<br>bid: " + b + "<br>aid: " + a + "<br>wid: " + w, ex);
            }
        }

    }
}
