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
    public class TeamViewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamViewModel(
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

        public Team _Team { get; set; }
        public bool _TeamOwner { get; set; } = false;
        public List<AnswerHistory> _AnswerHistory { get; set; }
        public List<TeamUser> _TeamUserList { get; set; }
        public List<C02系統値_TeamUser> _C02系統値_TeamUserAll { get; set; } = new List<C02系統値_TeamUser>();
        public List<string> _RaderJS_Data { get; set; }


        public string StatusMessage { get; set; }

        // id：TeamNo
        public async Task<IActionResult> OnGet(int? id)
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
                var teamNo = (int)id;

                // チームのメンバーじゃなければ、TeamView画面を表示させない。
                if (SP_JoinTeam.SelectCnt(_dbContext, userNo, teamNo) < 1)
                {
                    throw new Exception("ありえない");
                }

                HttpContext.Session.SetInt32(SessionKey.EditTeamNo, teamNo);

                _Team = SP_Team.Select(_dbContext, teamNo)[0];

                if (_Team.CreateUserNo == userNo)
                {
                    _TeamOwner = true;
                }

                _Team = SP_Team.Select(_dbContext, teamNo)[0];
                _TeamUserList = SP_JoinTeam.SelectUser(_dbContext, _Team.Id);
                _C02系統値_TeamUserAll = SP_C02系統値.SelectTeamUserAll(_dbContext, _Team.Id);

                _RaderJS_Data = new List<string>();
                var colorCnt = 0;
                foreach (var data in _C02系統値_TeamUserAll)
                {
                    //_RaderJS_Data.Add("{ 'label': 'My First Dataset', 'data': [65, 59, 90, 81, 56, 55, 40], 'fill': true, 'backgroundColor': 'rgba(255, 99, 132, 0.2)', 'borderColor': 'rgb(255, 99, 132)', 'pointBackgroundColor': 'rgb(255, 99, 132)', 'pointBorderColor': '#fff', 'pointHoverBackgroundColor': '#fff', 'pointHoverBorderColor': 'rgb(255, 99, 132)' }");
                    _RaderJS_Data.Add("{ " + $"'label': '{data.Nickname}', 'data': [{data.抑うつ性}, {data.気分の変化}, {data.劣等感}, {data.神経質}, {data.主観性}, {data.協調性}, {data.攻撃性}, {data.活動性}, {data.のん気}, {data.思考性}, {data.支配性}, {data.社会性}], 'fill': true, 'backgroundColor': 'rgba(" + WebColor.RGB_JS[colorCnt] + ", 0.2)', 'borderColor': 'rgb(" + WebColor.RGB_JS[colorCnt] + ")', 'pointBackgroundColor': 'rgb(" + WebColor.RGB_JS[colorCnt] + ")', 'pointBorderColor': '#fff', 'pointHoverBackgroundColor': '#fff', 'pointHoverBorderColor': 'rgb(" + WebColor.RGB_JS[colorCnt] + ")'" + " },");

                    colorCnt++;

                    if (WebColor.RGB_JS.Count < colorCnt)
                    {
                        colorCnt = 0;
                    }
                }

                StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage);
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                HttpContext.Session.Set(SessionKey.TeamEdit_SessionModel,
                    new TeamEdit_SessionModel() { TeamNo = teamNo });

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }


        public async Task<IActionResult> OnPostEdit()
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

                var sessionValue = HttpContext.Session.Get<TeamEdit_SessionModel>(SessionKey.TeamEdit_SessionModel);
                sessionValue.EditMode = 編集モード.編集;
                HttpContext.Session.Set(SessionKey.TeamEdit_SessionModel, sessionValue);

                return LocalRedirect("/TeamEdit");
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

            return BadRequest();
        }

    }
}
