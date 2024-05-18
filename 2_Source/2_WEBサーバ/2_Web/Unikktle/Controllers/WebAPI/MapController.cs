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
    public class MapController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;

        public MapController(
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

        // GET: https://localhost:44376/Map/{id}?vNo={vNo}?ExcludeId={ExcludeId}   
        // 例 https://localhost:44376/Map/1062747?vNo=1&ExcludeId=1
        // 変数名は「No」禁止。「id」じゃないとnullになる。
        // i：id
        // e：ExcludeId
        // p：pageNum
        [HttpGet("{i}", Name = "GetMap")]
        public async Task<SvgWordMapJS> Get(long? i, long? e, int? p)
        {
            try
            {
                if (i == null || p == null)
                {
                    return null;
                }

                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                // クリック履歴
                if (SessionCache.IsExistHistory(HttpContext.Session, (long)i) == false)
                {
                    SP_RelationWordClickHistory.Insert(_dbContext, (long)i, userNo,
                        HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                }

                return InMemoryCache.GetSvgWordMapJS(_dbContext, _Cache, (long)i, e, (int)p);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + i + "<br>ExcludeId: " + e + "<br>pageNum: " + p, ex);
            }

            return new SvgWordMapJS();
        }

    }
}

