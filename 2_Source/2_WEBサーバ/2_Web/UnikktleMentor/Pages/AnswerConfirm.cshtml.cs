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

namespace UnikktleMentor.Pages.Home
{
    public class AnswerConfirm : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AnswerConfirm(
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


        public List<Question_Answer> Question_AnswerList { get; set; }
        public AnswerDetailSupplement _AnswerDetailSupplement { get; set; }
        

        public TimeSpan 回答時間_分;

        public string _GenderName;
        public string _CareerName;

        public async Task<IActionResult> OnGet(int? id, int? id2)
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

                var fromTeamView = (Request.Headers["Referer"].ToString().IndexOf("/TeamViewUser?") > -1);
                //List<AnswerDetail> answerSelect;
                long userNo;

                if (fromTeamView && id2 == null)
                {
                    // TeamView画面から遷移して来て、診断対象者ID（id2）がnullのケースはありえない。
                    throw new Exception("ありえない");
                }
                else if (fromTeamView)
                {
                    // TeamView画面から遷移して来た場合、userNoを動的に変える。
                    userNo = (long)id2;
                }
                else
                {
                    // デフォルト値は、ログインユーザーのuserNo。
                    userNo = (long)no;
                }

                var questionList = InMemoryCache.Question(_Cache).QuestionList;

                Question_AnswerList = new List<Question_Answer>();

                foreach (var item in questionList)
                {
                    Question_AnswerList.Add(new Question_Answer()
                    {
                        AnswerNo = item.Id,
                        Setsumon = item.Setsumon
                    });
                }

                var answerList = SP_AnswerDetail.Select(_dbContext, userNo, (int)id);

                foreach (var item in Question_AnswerList)
                {
                    item.Answer = ControlCommon.GetAnswerString(answerList.First(x => x.Id == item.AnswerNo).回答);
                }

                _AnswerDetailSupplement = SP_AnswerDetailSupplement.Select(_dbContext, userNo, (int)id)[0];

                if (_AnswerDetailSupplement.Gender == Gender.Male)
                {
                    // 男性
                    _GenderName = _sharedLocalizer["2"];
                }
                else
                {
                    // 女性
                    _GenderName = _sharedLocalizer["3"];
                }

                _CareerName = InMemoryCache.GetCareerAllList(_dbContext, _Cache).First(x => x.Id == _AnswerDetailSupplement.Career).Name;

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        public IActionResult OnPostBackAsync()
        {
            try
            {
                // 未ログインで診断のみするケースがある。
                //(bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                //if (bad)
                //{
                //    return LocalRedirect("/Identity/Account/Login");
                //}

                //var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                //if (no == null)
                //{
                //    return BadRequest();
                //}
                //var userNo = (long)no;

                return LocalRedirect("/Home");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

    }
}