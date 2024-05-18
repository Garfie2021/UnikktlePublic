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

namespace UnikktleMentor.Pages.Mind
{

    public class TeamViewUser : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamViewUser(
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


        public List<AnswerHistory> _AnswerHistory;
        public List<AnswerHistory> _AnswerHistory_Desc;

        public List<C02系統値> _C02結果List;

        public HomeGetModel _HomeGetModel;

        public long TargetUserID;

        // id ： チームに所属している、参照対象のユーザID。
        public async Task<IActionResult> OnGet(long? id)
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    throw new Exception("ありえない");
                }
                var userNo = (long)no;

                var fromTeamView = (Request.Headers["Referer"].ToString().IndexOf("/TeamView?") > -1);
                if (fromTeamView && id == null)
                {
                    // TeamView画面から遷移して来て、診断対象者ID（id）がnullのケースはありえない。
                    throw new Exception("ありえない");
                }

                TargetUserID = (long)id;

                _AnswerHistory = SP_AnswerHistory.Select(_dbContext, TargetUserID);
                _C02結果List = SP_C02系統値.Select_AllDate(_dbContext, TargetUserID);

                _HomeGetModel = ControlCommon.HomeOnGet(_AnswerHistory, _C02結果List);

                _AnswerHistory_Desc = SP_AnswerHistory.Select_Desc(_dbContext, TargetUserID);

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

    }
}
