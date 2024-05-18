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

    public class HomeModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeModel(
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

        public List<Team> _TeamCreated { get; set; }

        public List<Team_TeamID> _TeamBelongs { get; set; }

        public string TeamName { get; set; }

        public string StatusMessage { get; set; }


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

                _AnswerHistory = SP_AnswerHistory.Select(_dbContext, userNo);
                _C02結果List = SP_C02系統値.Select_AllDate(_dbContext, userNo);

                _HomeGetModel = ControlCommon.HomeOnGet(_AnswerHistory, _C02結果List);

                _AnswerHistory_Desc = SP_AnswerHistory.Select_Desc(_dbContext, userNo);

                // 作成したチーム
                _TeamCreated = SP_Team.Select_CreatedTeam(_dbContext, userNo);

                // 所属チーム
                _TeamBelongs = SP_JoinTeam.Select(_dbContext, userNo);

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

        public async Task<IActionResult> OnPostAnswerNewStartAsync()
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

                ControlCommon.InitalizeSession_Answer(HttpContext.Session);

                return LocalRedirect("/AnswerNew");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }


        public async Task<IActionResult> OnPostAnswerConfirmAsync(int Id)
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

                return LocalRedirect("/AnswerConfirm?id=" + Id);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostDiagnosisAsync(int Id)
        {
            try
            {
                //Console.WriteLine("HomeModel OnPostDiagnosisAsync 1");

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                //Console.WriteLine("HomeModel OnPostDiagnosisAsync 2");

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    throw new Exception("ありえない");
                }
                var userNo = (long)no;

                //Console.WriteLine("HomeModel OnPostDiagnosisAsync 3");

                return LocalRedirect("/Diagnosis?id=" + Id);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            //Console.WriteLine("HomeModel OnPostDiagnosisAsync 5");
            return BadRequest();
        }

        public async Task<IActionResult> OnPostTeamEdit()
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
                
                HttpContext.Session.Set(SessionKey.TeamEdit_SessionModel,
                    new TeamEdit_SessionModel() { 
                        EditMode = 編集モード.新規作成
                    } );

                return LocalRedirect("/TeamEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostTeamView(int? id)
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

                return LocalRedirect($"/TeamView?id=" + id.ToString() + "");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return Page();
        }

    }
}
