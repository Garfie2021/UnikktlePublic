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
    public class AnswerNewConfirm : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AnswerNewConfirm(
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

        public InputModel_Answer_Supplement AnswerSupplement { get; set; }

        public TimeSpan 回答時間_分;

        public string _GenderName;
        public string _CareerName;

        public UserSetting _UserSettingProfile;

        public async Task<IActionResult> OnGet()
        {
            try
            {
                // 未ログインで診断のみするケースがある。
                // ログインしている場合、ユーザープロファイルを取得する。
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (!bad)
                {
                    var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                    if (no != null)
                    {
                        var userNo = (long)no;

                        _UserSettingProfile = SessionCache.GetUserSettingProfile(
                            HttpContext.Session, _dbContext, _Cache, _userManager, _emailSender, User);
                    }
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

                var list = HttpContext.Session.Get<List<InputModel_AnswerEdit>>(SessionKey.AnswerList);

                var pageCnt = 0;
                foreach (var item in list)
                {
                    var pageNum = pageCnt * 20;
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A0);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A1);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A2);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A3);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A4);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A5);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A6);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A7);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A8);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A9);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A10);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A11);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A12);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A13);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A14);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A15);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A16);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A17);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A18);
                    Question_AnswerList[pageNum++].Answer = ControlCommon.GetAnswerString((回答選択肢)item.A19);
                    pageCnt++;
                }

                AnswerSupplement = SessionValue.GetAnswerSupplement(HttpContext.Session);

                var answerNewEnd = DateTime.Now;
                HttpContext.Session.Set<DateTime>(SessionKey.AnswerNewEnd, answerNewEnd);

                var answerNewStart = HttpContext.Session.Get<DateTime>(SessionKey.AnswerNewStart);

                回答時間_分 = answerNewEnd - answerNewStart;

                if (AnswerSupplement._Gender == Gender.Male)
                {
                    // 男性
                    _GenderName = _sharedLocalizer["2"];
                }
                else
                {
                    // 女性
                    _GenderName = _sharedLocalizer["3"];
                }

                _CareerName = InMemoryCache.GetCareerAllList(_dbContext, _Cache).First(x => x.Id == AnswerSupplement.Career).Name;

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
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

                return LocalRedirect("/AnswerNewSupplement");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostCompleteAsync()
        {
            try
            {
                //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 1");

                var answerList = HttpContext.Session.Get<List<InputModel_AnswerEdit>>(SessionKey.AnswerList);
                var answerSupplement = HttpContext.Session.Get<InputModel_Answer_Supplement>(SessionKey.AnswerSupplement);
                var answerNewStart = HttpContext.Session.Get<DateTime>(SessionKey.AnswerNewStart);
                var answerNewEnd = HttpContext.Session.Get<DateTime>(SessionKey.AnswerNewEnd);

                if (answerList == null || answerSupplement == null || 
                    answerNewStart == null || answerNewEnd == null)
                {
                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 2");
                    return BadRequest();
                }

                byte cnt = 0;
                var AnswerList = new List<AnswerDetail>();
                foreach (var item in answerList)
                {
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A0 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A1 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A2 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A3 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A4 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A5 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A6 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A7 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A8 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A9 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A10 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A11 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A12 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A13 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A14 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A15 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A16 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A17 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A18 });
                    AnswerList.Add(new AnswerDetail() { Id = cnt++, 回答 = (回答選択肢)item.A19 });
                }

                //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 3");

                // 診断
                CharacterDiagnosis.Diagnosis(answerSupplement._Gender, AnswerList,
                    out C01粗点計算_結果 c01結果,
                    out C02系統値計算_結果 c02結果,
                    out C03因子得点_結果 c03因子結果,
                    out C03系統判定_結果 c03系統結果,
                    out C04基礎因子判定_結果 c04結果,
                    out C05基礎因子長短判定_結果 c05結果,
                    out C06関連因子判定_結果 c06結果,
                    out C07集合因子判定_結果 c07結果,
                    out C08類型別集合因子判定_結果 c08結果,
                    out C09ノイローゼ因子判定_結果 c09結果,
                    out C10リーダー資質判定_結果 c10結果,
                    out C11職種別適応性判定_結果 c11結果,
                    out string str診断結果,
                    out string strエラー);

                //var now = DateTime.Now;

                // 未ログインで診断のみするケースがある。
                long? userNo = null;
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (!bad)
                {
                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 4");

                    userNo = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                }

                //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 5");

                if (userNo == null)
                {
                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 6");

                    // 未ログイン

                    HttpContext.Session.Set<C01粗点計算_結果>(SessionKey.C01結果, c01結果);
                    HttpContext.Session.Set<C02系統値計算_結果>(SessionKey.C02結果, c02結果);
                    HttpContext.Session.Set<C04基礎因子判定_結果>(SessionKey.C04結果, c04結果);
                    HttpContext.Session.Set<C07集合因子判定_結果>(SessionKey.C07結果, c07結果);
                    HttpContext.Session.Set<C09ノイローゼ因子判定_結果>(SessionKey.C09結果, c09結果);
                    HttpContext.Session.Set<C10リーダー資質判定_結果>(SessionKey.C10結果, c10結果);
                    HttpContext.Session.Set<DateTime>(SessionKey.LastDiagnosisDate, answerNewEnd);

                    SP_AnswerHistoryNoLogin.InsertUpdate(_dbContext, HttpContext.Session.Id, answerNewStart, answerNewEnd);

                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 7");

                    return LocalRedirect("/Diagnosis");
                }
                else
                {
                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 8");

                    // 回答をDB登録
                    var answerId = SP_AnswerHistory.Insert(_dbContext, (long)userNo, answerNewStart, answerNewEnd);

                    SP_AnswerDetailSupplement.Insert(_dbContext, (long)userNo, answerId, answerSupplement);

                    SP_UserSetting.UpdateProfile2(_dbContext, (long)userNo,
                        answerSupplement._Gender, answerSupplement.Career);

                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 9");

                    BulkCopy_Answer.Flush(_dbContext.Database.GetDbConnection(), (long)userNo, answerId, answerList);

                    // 診断結果をDB登録
                    SP_C02系統値.Insert(_dbContext, (long)userNo, answerId, c02結果);

                    //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 10");

                    return LocalRedirect("/Diagnosis?id=" + answerId);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 11");

                ExceptionSt.ExceptionCommon(ex);
            }
            finally
            {
                //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 12");

                HttpContext.Session.Remove(SessionKey.CurrentPage);
                HttpContext.Session.Remove(SessionKey.AnswerList);
                HttpContext.Session.Remove(SessionKey.AnswerSupplement);
                HttpContext.Session.Remove(SessionKey.AnswerNewStart);
                HttpContext.Session.Remove(SessionKey.AnswerNewEnd);
            }

            //Console.WriteLine("AnswerNewConfirm OnPostCompleteAsync 13");

            return BadRequest();
        }
    }
}