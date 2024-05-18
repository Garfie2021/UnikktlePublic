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
    public class MapPRController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;

        public MapPRController(
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
        // i：id
        // p：pageNum
        [HttpGet("{i}", Name = "GetMapPR")]
        public async Task<AdverSelectPrRelation> Get(long? i, int? p)
        {
            try
            {
                if (i == null || p == null)
                {
                    return null;
                }

                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                // 広告
                return ControlCommon.GetRelationPR(_dbContext,
                    (long)i, userNo,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    (int)p + 1);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + i + "<br>pageNum: " + p, ex);
            }

            return new AdverSelectPrRelation();
        }

    }
}
