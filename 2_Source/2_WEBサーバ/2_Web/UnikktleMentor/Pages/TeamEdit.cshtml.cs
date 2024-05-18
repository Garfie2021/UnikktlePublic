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
    public class TeamEditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamEditModel(
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

        //public Team _Team { get; set; }

        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel_TeamEdit Input { get; set; }

        public List<TeamUser> _TeamUserList { get; set; }

        public TeamEdit_SessionModel _TeamEdit_SessionModel { get; set; }
        
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

                _TeamEdit_SessionModel = HttpContext.Session.Get<TeamEdit_SessionModel>(SessionKey.TeamEdit_SessionModel);
                if (_TeamEdit_SessionModel == null)
                {
                    throw new Exception("ありえない");
                }

                if (_TeamEdit_SessionModel.EditMode == 編集モード.新規作成)
                {
                    // 新規作成

                    //Input = new InputModel_TeamEdit();
                    //_Team = new Team();
                }
                else
                {
                    // 編集

                    var team = SP_Team.Select(_dbContext, _TeamEdit_SessionModel.TeamNo)[0];

                    Input = new InputModel_TeamEdit
                    {
                        TeamNo = team.Id,
                        TeamID = team.TeamID,
                        TeamExplanation = team.TeamExplanation,
                        AllowPublic = team.AllowPublic,
                        AllowApplyToJoinTeam = team.AllowApplyToJoinTeam
                    };

                    _TeamUserList = SP_JoinTeam.SelectUser(_dbContext, team.Id);

                    HttpContext.Session.SetInt32(SessionKey.EditTeamNo, _TeamEdit_SessionModel.TeamNo);
                }

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

        // id：TeamNo
        public async Task<IActionResult> OnPostSave()
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

                _TeamEdit_SessionModel = HttpContext.Session.Get<TeamEdit_SessionModel>(SessionKey.TeamEdit_SessionModel);

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (_TeamEdit_SessionModel == null)
                {
                    throw new Exception("ありえない");
                }

                if (_TeamEdit_SessionModel.EditMode == 編集モード.新規作成)
                {
                    // 新規作成

                    if (SP_Team.IsExistence(_dbContext, Input))
                    {
                        // Error. 入力されたIDは、既に使用されています。
                        HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["30"]);
                        return Page();
                    }

                    if (SP_Team.SelectCount_CreateUserNo(_dbContext, userNo) > 2)
                    {
                        // Error. 1ユーザが作成できるチーム数は最大3つまでです。
                        HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["31"]);
                        return Page();
                    }

                    var teamNo = SP_Team.Insert(_dbContext, userNo, Input);
                    SP_JoinTeam.Insert(_dbContext, userNo, teamNo);

                    // チームを作成しました。
                    HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["32"]);

                    HttpContext.Session.Remove(SessionKey.TeamEdit_SessionModel);

                    return LocalRedirect("/TeamView?id=" + teamNo);
                }
                else
                {
                    // 編集

                    // Teamの作成者かチェックし、違う場合は不正アクセスとみなす。
                    if (SP_Team.ChkTeamOwner(_dbContext, userNo, _TeamEdit_SessionModel.TeamNo) == false)
                    {
                        throw new Exception("ありえない");
                    }

                    SP_Team.Update(_dbContext, userNo, _TeamEdit_SessionModel.TeamNo, Input);

                    // チームを更新しました。
                    HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["33"]);

                    HttpContext.Session.Remove(SessionKey.TeamEdit_SessionModel);

                    return LocalRedirect("/TeamView?id=" + _TeamEdit_SessionModel.TeamNo);
                }
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        // id：削除対象のユーザID
        public async Task<IActionResult> OnPostDeleteMember(long? id)
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

                if (id == null)
                {
                    // idがnullのパターンは存在しない。
                    throw new Exception("ありえない");
                }

                var teamNo = HttpContext.Session.GetInt32(SessionKey.EditTeamNo);

                SP_JoinTeam.DeleteMember(_dbContext, (int)teamNo, (long)id);

                // チームのメンバーを削除しました。
                HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["34"]);

                return LocalRedirect("/TeamEdit?id="+ teamNo);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostDelete(int? id)
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

                if (id == null)
                {
                    throw new Exception("ありえない");
                }
                var teamId = (int)id;

                if (SP_Team.ChkTeamOwner(_dbContext, userNo, teamId) == false)
                {
                    // 削除する権限が無い
                    throw new Exception("ありえない");
                }

                SP_Team.Delete(_dbContext, teamId);
                SP_JoinTeam.Delete(_dbContext, teamId);

                // チームを削除しました。
                HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["35"]);

                return LocalRedirect("/Home");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        // id：参加許可対象のユーザID
        public async Task<IActionResult> OnPostAllowParticipation(long? id)
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

                var teamNo = HttpContext.Session.GetInt32(SessionKey.EditTeamNo);

                if (id == null || teamNo == null)
                {
                    // id/teamNoがnullのパターンは存在しない。
                    throw new Exception("ありえない");
                }

                SP_JoinTeam.Update_Status(_dbContext, (long)id, (int)teamNo, JoinTeamStatus.参加済み);

                var teamID = SP_Team.Select(_dbContext, (int)teamNo)[0].TeamID;
                var email = SP_UserSetting.SelectProfile(_dbContext, (long)id)[0].Email;

                await _emailSender.SendEmailAsync(email,
                    // チーム参加申請結果
                    _sharedLocalizer["74"],
                    // {0}チームへの参加申請が許可されました。
                    _sharedLocalizer["75", teamID]);

                // 参加を許可しました。
                HttpContext.Session.SetString(SessionKey.StatusMessage, _sharedLocalizer["36"]);

                return LocalRedirect("/TeamView?id=" + teamNo);
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
                _TeamEdit_SessionModel = HttpContext.Session.Get<TeamEdit_SessionModel>(SessionKey.TeamEdit_SessionModel);
                if (_TeamEdit_SessionModel == null)
                {
                    throw new Exception("ありえない");
                }

                if (_TeamEdit_SessionModel.EditMode == 編集モード.新規作成)
                {
                    // 新規作成

                    return LocalRedirect("/Home");
                }
                else
                {
                    // 編集

                    return LocalRedirect("/TeamView?id=" + _TeamEdit_SessionModel.TeamNo);
                }
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }
    }
}
