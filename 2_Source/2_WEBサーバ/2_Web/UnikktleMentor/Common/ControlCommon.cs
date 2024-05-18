using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Common;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using UnikktleMentor.Cache;
using UnikktleMentorEngine;

namespace UnikktleMentor.Common
{
    public static class ControlCommon
    {
        public static string NullToEmptyString(string value)
        {
            return value == null ? "" : value;
        }

        public static HomeGetModel HomeOnGet(List<AnswerHistory> _AnswerHistory, List<C02系統値> _C02結果List)
        {
            var homeGetModel = new HomeGetModel();

            foreach (var item in _AnswerHistory)
            {
                homeGetModel._X_Axis += "'" + item.AnswerDate.ToString("yyyyMMdd") + "',";
            }

            foreach (var item in _C02結果List)
            {
                homeGetModel._抑うつ性 += item.D + ",";
                homeGetModel._気分の変化 += item.C + ",";
                homeGetModel._劣等感 += item.I + ",";
                homeGetModel._神経質 += item.N + ",";
                homeGetModel._主観性 += item.O + ",";
                homeGetModel._協調性 += item.Co + ",";
                homeGetModel._攻撃性 += item.Ag + ",";
                homeGetModel._活動性 += item.G + ",";
                homeGetModel._のん気 += item.R + ",";
                homeGetModel._思考性 += item.T + ",";
                homeGetModel._支配性 += item.A + ",";
                homeGetModel._社会性 += item.S + ",";
            }

            return homeGetModel;
        }


        // 質問回答に関係するセッションを、回答開始時に初期化する。
        public static void InitalizeSession_Answer(ISession session)
        {
            session.SetInt32(SessionKey.CurrentPage, 0);
            session.Remove(SessionKey.AnswerList);
            session.Set<DateTime>(SessionKey.AnswerNewStart, DateTime.Now);
        }

        // ToDo:ローカライズするのでenum化していない。
        public static string GetAnswerString(回答選択肢 answer)
        {
            if (answer == 回答選択肢.はい)
            {
                return "はい";
            }
            else if (answer == 回答選択肢.いいえ)
            {
                return "いいえ";
            }
            else if (answer == 回答選択肢.どちらともいえない)
            {
                return "どちらともいえない";
            }
            else
            {
                return "未回答";
            }
        }

        public static async Task<long> GetUserNo(
            ApplicationDbContext dbContext, 
            UserManager<IdentityUser> userManager, ClaimsPrincipal User,
            ISession Session, IEmailSender emailSender)
        {
            (bool bad, IdentityUser user) = await AuthenticatBadAsync(userManager, User);
            if (bad)
            {
                // 未ログイン
                return -1;
            }

            var no2 = SessionCache.GetUserNo(dbContext, Session, emailSender, user.Id);
            if (no2 == null)
            {
                // 未ログイン
                return -1;
            }

            // ログイン済み
            return (long)no2;
        }


        public static async Task<(bool, IdentityUser)> AuthenticatBadAsync(UserManager<IdentityUser> userManager, ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return (true, null);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return (true, null);
            }

            return (false, user);
        }

        public static List<CareerCategory> GetCareerAndCategoryList_JA(//ISession session,
            ApplicationDbContext dbContext)
        {
            var CareerList = new List<CareerCategory>();

            var careerCategory = SP_Attribute.SelectClass_JA(dbContext, AttributeClass.CareerCategory);

            foreach (var category in careerCategory)
            {
                var career = SP_Career.Select_JA(dbContext, category.Id);

                CareerList.Add(new CareerCategory()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CareerList = career
                });
            }

            return CareerList;
        }

        public static List<CareerCategory> GetCareerAndCategoryList_EN(//ISession session,
            ApplicationDbContext dbContext)
        {
            var CareerList = new List<CareerCategory>();

            var careerCategory = SP_Attribute.SelectClass_EN(dbContext, AttributeClass.CareerCategory);

            foreach (var category in careerCategory)
            {
                var career = SP_Career.Select_EN(dbContext, category.Id);

                CareerList.Add(new CareerCategory()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CareerList = career
                });
            }

            return CareerList;
        }

        public static List<Career> GetCareerAllList_JA(ApplicationDbContext dbContext)
        {
            var career = SP_Career.SelectAll_JA(dbContext);

            return career;
        }

        public static List<Career> GetCareerAllList_EN(ApplicationDbContext dbContext)
        {
            var career = SP_Career.SelectAll_EN(dbContext);

            return career;
        }
    }
}
