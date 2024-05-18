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

    public class TeamJoinSettingModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamJoinSettingModel(
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

        public string TeamName { get; set; }

        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel_TeamJoinEdit Input { get; set; }

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

                StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage);
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostJoinComfirmAsync()
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

                if (Input.TeamID == null)
                {
                    // Error. チームIDが入力されていません。
                    HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["38"]);
                    return Page();
                }

                var team = SP_Team.Select_TeamID(_dbContext, Input.TeamID);

                if (team.Count() < 1)
                {
                    // Error. 該当するチームが見つかりませんでした。
                    HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["39"]);
                    return Page();
                }

                if (team[0].AllowApplyToJoinTeam == false)
                {
                    // Error. {Input.TeamID}チームは、参加申請を受け付けていません。
                    HttpContext.Session.SetString(SessionKey.StatusMessage, string.Format(_sharedLocalizer["37"], Input.TeamID));
                    return Page();
                }

                // チームに参加済みかチェック
                var result = SP_JoinTeam.SelectStatus(_dbContext, userNo, team[0].Id);

                if (result != null)
                {
                    var joinTeamStatus = (JoinTeamStatus)result;
                    if (joinTeamStatus == JoinTeamStatus.参加申請中)
                    {
                        // Error. 既に参加申請済みです。
                        HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["42"]);
                    }
                    else
                    {
                        // Error. 既に参加済みです。
                        HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["43"]);
                    }

                    return Page();
                }

                // ToDo:そのうち作る。
                //if (SP_Team.SelectAllowApplyToJoinTeam(_dbContext, Input.TeamID) == false)
                //{
                //    // ブラックリスト。
                //    StatusMessage = $"Error. {Input.TeamID}チームから参加申請を拒否されました。";
                //    return Page();
                //}

                HttpContext.Session.Set(SessionKey.TeamJoinEdit, new TeamJoinEdit()
                {
                    TeamID = Input.TeamID,
                    AllowDiagnosisPublicToTheTeamReader = Input.AllowDiagnosisPublicToTheTeamReader,
                    AllowDiagnosisPublicToTheTeamMember = Input.AllowDiagnosisPublicToTheTeamMember
                });

                return LocalRedirect("/TeamJoinComfirm");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostJoinAsync(int Id)
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
                return LocalRedirect("/Home");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return Page();
        }
    }
}
