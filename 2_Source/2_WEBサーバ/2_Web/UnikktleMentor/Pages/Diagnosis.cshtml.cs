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
    public class Diagnosis : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public Diagnosis(
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


        public C01粗点計算_結果 C01結果;
        public C02系統値計算_結果 C02結果;
        public C03因子得点_結果 C03因子結果;
        public C03系統判定_結果 C03系統結果;
        public C04基礎因子判定_結果 C04結果;
        public C05基礎因子長短判定_結果 C05結果;
        public C06関連因子判定_結果 C06結果;
        public C07集合因子判定_結果 C07結果;
        public C08類型別集合因子判定_結果 C08結果;
        public C09ノイローゼ因子判定_結果 C09結果;
        public C10リーダー資質判定_結果 C10結果;
        public C11職種別適応性判定_結果 C11結果;
        public string 診断結果;
        public string エラー;

        public UserSetting _UserSettingProfile;
        public List<string> _RaderJS_Data { get; set; }

        // id :  AnswerId。診断結果ID。
        // id2 : user.Id。診断を受けたユーザのID。TeamView.cshtmlから所属メンバの診断を見る際に使われる。
        public async Task<IActionResult> OnGet(int? id, long? id2)
        {
            try
            {
                //Console.WriteLine("Diagnosis OnGet 1");

                if (id == null)
                {
                    //Console.WriteLine("Diagnosis OnGet 2");
                    // 未ログイン

                    C01結果 = HttpContext.Session.Get<C01粗点計算_結果>(SessionKey.C01結果);
                    C02結果 = HttpContext.Session.Get<C02系統値計算_結果>(SessionKey.C02結果);
                    C04結果 = HttpContext.Session.Get<C04基礎因子判定_結果>(SessionKey.C04結果);
                    C07結果 = HttpContext.Session.Get<C07集合因子判定_結果>(SessionKey.C07結果);
                    C09結果 = HttpContext.Session.Get<C09ノイローゼ因子判定_結果>(SessionKey.C09結果);
                    C10結果 = HttpContext.Session.Get<C10リーダー資質判定_結果>(SessionKey.C10結果);

                    //Console.WriteLine("Diagnosis OnGet 3");
                    if (C01結果 == null || C02結果 == null || C04結果 == null || C07結果 == null ||
                        C09結果 == null || C10結果 == null)
                    {
                        //Console.WriteLine("Diagnosis OnGet 4");
                        // URL直接指定からの遷移時。回答されていないのでトップ画面に遷移する。
                        return LocalRedirect("/");
                    }

                    //Console.WriteLine("Diagnosis OnGet 5");
                    _RaderJS_Data = new List<string>();
                    _RaderJS_Data.Add("{ " + $"'label': '', 'data': [{C02結果.標準点D_抑うつ性}, {C02結果.標準点C_気分の変化}, {C02結果.標準点I_劣等感}, {C02結果.標準点N_神経質}, {C02結果.標準点O_主観性}, {C02結果.標準点Co_協調性}, {C02結果.標準点Ag_攻撃性}, {C02結果.標準点G_活動性}, {C02結果.標準点R_のん気}, {C02結果.標準点T_思考性}, {C02結果.標準点A_支配性}, {C02結果.標準点S_社会性}], 'fill': true, 'backgroundColor': 'rgba(" + WebColor.RGB_JS[0] + ", 0.2)', 'borderColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBackgroundColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBorderColor': '#fff', 'pointHoverBackgroundColor': '#fff', 'pointHoverBorderColor': 'rgb(" + WebColor.RGB_JS[0] + ")'" + " },");
                }
                else
                {
                    // ログイン済み

                    //Console.WriteLine("Diagnosis OnGet 6");
                    (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                    if (bad)
                    {
                        //Console.WriteLine("Diagnosis OnGet 7");
                        return LocalRedirect("/Identity/Account/Login");
                    }

                    var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                    if (no == null)
                    {
                        //Console.WriteLine("Diagnosis OnGet 8");
                        throw new Exception("ありえない");
                    }

                    //Console.WriteLine("Diagnosis OnGet 9");
                    var fromTeamView = (Request.Headers["Referer"].ToString().IndexOf("/TeamViewUser?") > -1);
                    List<AnswerDetail> answerSelect;
                    long userNo;

                    if (fromTeamView && id2 == null)
                    {
                        //Console.WriteLine("Diagnosis OnGet 10");
                        // TeamView画面から遷移して来て、診断対象者ID（id2）がnullのケースはありえない。
                        throw new Exception("ありえない");
                    }
                    else if (fromTeamView)
                    {
                        //Console.WriteLine("Diagnosis OnGet 11");
                        // TeamView画面から遷移して来た場合、userNoを動的に変える。
                        userNo = (long)id2;
                    }
                    else
                    {
                        //Console.WriteLine("Diagnosis OnGet 12");
                        // デフォルト値は、ログインユーザーのuserNo。
                        userNo = (long)no;
                    }

                    //Console.WriteLine("Diagnosis OnGet 13");
                    answerSelect = SP_AnswerDetail.Select(_dbContext, userNo, (int)id);

                    //Console.WriteLine("Diagnosis OnGet 13-1");
                    CharacterDiagnosis.Diagnosis(Gender.Male, answerSelect,
                        out C01結果,
                        out C02結果,
                        out C03因子結果,
                        out C03系統結果,
                        out C04結果,
                        out C05結果,
                        out C06結果,
                        out C07結果,
                        out C08結果,
                        out C09結果,
                        out C10結果,
                        out C11結果,
                        out 診断結果,
                        out エラー);

                    //Console.WriteLine("Diagnosis OnGet 14");
                    _RaderJS_Data = new List<string>();
                    _RaderJS_Data.Add("{ " + $"'label': '', 'data': [{C02結果.標準点D_抑うつ性}, {C02結果.標準点C_気分の変化}, {C02結果.標準点I_劣等感}, {C02結果.標準点N_神経質}, {C02結果.標準点O_主観性}, {C02結果.標準点Co_協調性}, {C02結果.標準点Ag_攻撃性}, {C02結果.標準点G_活動性}, {C02結果.標準点R_のん気}, {C02結果.標準点T_思考性}, {C02結果.標準点A_支配性}, {C02結果.標準点S_社会性}], 'fill': true, 'backgroundColor': 'rgba(" + WebColor.RGB_JS[0] + ", 0.2)', 'borderColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBackgroundColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBorderColor': '#fff', 'pointHoverBackgroundColor': '#fff', 'pointHoverBorderColor': 'rgb(" + WebColor.RGB_JS[0] + ")'" + " },");

                    //Console.WriteLine("Diagnosis OnGet 15");
                    _UserSettingProfile = SessionCache.GetUserSettingProfile(
                        HttpContext.Session, _dbContext, _Cache, _userManager, _emailSender, User);
                }

                //Console.WriteLine("Diagnosis OnGet 16");
                return Page();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Diagnosis OnGet 17");
                ExceptionSt.ExceptionCommon(ex);
            }

            //Console.WriteLine("Diagnosis OnGet 18");
            return BadRequest();
        }
    }
}
