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


namespace UnikktleMentor.Pages
{

    public class TeamJoinComfirmModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamJoinComfirmModel(
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

        public string _TeamName { get; set; }

        public string _AllowDiagnosisPublicToTheTeamReader { get; set; }
        public string _AllowDiagnosisPublicToTheTeamMember { get; set; }


        public TeamJoinEdit _TeamJoinEdit { get; set; }
        
        [BindProperty]
        public InputModel_TeamEdit Input { get; set; }

        public async Task<IActionResult> OnGet()
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

                _TeamJoinEdit = HttpContext.Session.Get<TeamJoinEdit>(SessionKey.TeamJoinEdit);

                // はい　いいえ
                _AllowDiagnosisPublicToTheTeamReader =
                    _TeamJoinEdit.AllowDiagnosisPublicToTheTeamReader ? _sharedLocalizer["4"] : _sharedLocalizer["5"];

                // はい　いいえ
                _AllowDiagnosisPublicToTheTeamMember =
                    _TeamJoinEdit.AllowDiagnosisPublicToTheTeamMember ? _sharedLocalizer["4"] : _sharedLocalizer["5"];

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostJoinExecAsync(int Id)
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

                _TeamJoinEdit = HttpContext.Session.Get<TeamJoinEdit>(SessionKey.TeamJoinEdit);

                SP_JoinTeam.Insert_TeamID(_dbContext, userNo, _TeamJoinEdit.TeamID);

                HttpContext.Session.Remove(SessionKey.TeamJoinEdit);

                // チームオーナーのメールアドレスを取得
                var createUserMailAddress = SP_Team.Select_CreateUserMailAddress(_dbContext, _TeamJoinEdit.TeamID);
                var userProfile = SP_UserSetting.SelectProfile(_dbContext, userNo)[0];

                await _emailSender.SendEmailAsync(createUserMailAddress,
                    // チーム参加申請
                    _sharedLocalizer["6"],
                    // 〇〇ユーザから、〇〇チームに参加申請が届いてます。
                    // チームの編集画面で、〇〇ユーザからの参加申請を許可するか、参加申請を拒否するか選択して下さい。
                    _sharedLocalizer["7", userProfile.Nickname, _TeamJoinEdit.TeamID]);

                return LocalRedirect("/Home");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public IActionResult OnPostBack()
        {
            try
            {
                return LocalRedirect("/TeamJoinEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return Page();
        }
    }
}
