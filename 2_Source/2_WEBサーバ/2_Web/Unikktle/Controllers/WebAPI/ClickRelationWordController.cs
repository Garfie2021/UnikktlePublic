// 使ってない

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
//using Unikktle.Cache;
//using Unikktle.Common;
//using Unikktle.Data;
//using Unikktle.Models;

//namespace Unikktle.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class ClickRelationWordController : ControllerBase
//    {
//        private readonly ApplicationDbContext _dbContext;
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly IMemoryCache _Cache;
//        private readonly IEmailSender _emailSender;

//        public ClickRelationWordController(
//            ApplicationDbContext dbContext,
//            UserManager<IdentityUser> userManager,
//            IMemoryCache cache,
//            IEmailSender emailSender)
//        {
//            _dbContext = dbContext;
//            _userManager = userManager;
//            _Cache = cache;
//            _emailSender = emailSender;
//        }

//        // GET: https://localhost:44376/PRClick?SearchString={SearchString}?pageNum={pageNum}    
//        // 例 https://localhost:44376/PRClick?SearchString=c%23&pageNum=0
//        //
//        // u: uid: UserNo 広告主のUser
//        // a: aid: AdverNo
//        // b: bid: BusinessNo
//        // w: wid: WordNo
//        [HttpGet]
//        public async Task Get(long? w)
//        {
//            try
//            {
//                if (w == null)
//                {
//                    return;
//                }

//                if (SessionCache.IsExistHistory(HttpContext.Session, (long)w))
//                {
//                    // 既にクリックされた。
//                    return;
//                }

//                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
//                    HttpContext.Session, _emailSender);

//                SP_RelationWordClickHistory.Insert(_dbContext,
//                    (long)w,
//                    userNo, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
//            }
//            catch (Exception ex)
//            {
//                MailNotify.ExceptionMail(_emailSender, "wid: " + w, ex);
//            }
//        }

//    }
//}
