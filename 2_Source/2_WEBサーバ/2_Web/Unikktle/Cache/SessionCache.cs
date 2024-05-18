using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Unikktle.Cache
{
    public static class SessionCache
    {
        public static bool IsExistHistory(ISession session,
            long uid, short bid, int aid, long wid)
        {
            var list = session.Get<List<ClickPR>>(SessionKey.ClickPRList);
            if (list == null)
            {
                list = new List<ClickPR>();
            }
            else
            {
                if (list.Any(x => x.uid == uid && x.bid == bid && x.aid == aid && x.wid == wid))
                {
                    // このセッションには既にクリック履歴がある。
                    return true;
                }
            }

            list.Add(new ClickPR()
            {
                uid = uid,
                bid = bid,
                aid = aid,
                wid = wid
            });

            session.Set(SessionKey.ClickPRList, list);

            return false;
        }

        public static bool IsExistHistory(ISession session, long wid)
        {
            var list = session.Get<List<long>>(SessionKey.ClickWordList);
            if (list == null)
            {
                list = new List<long>();
            }
            else
            {
                if (list.Any(x => x == wid))
                {
                    // このセッションには既にクリック履歴がある。
                    return true;
                }
            }

            list.Add(wid);

            session.Set(SessionKey.ClickWordList, list);

            return false;
        }

        // 未ログインを許容しない。
        public static long? GetUserNo(ApplicationDbContext dbContext,
           ISession session, IEmailSender emailSender,
           string userID)
        {
            // キャッシュ有無をチェック
            
            var userNo = session.GetString(SessionKey.UserNo);
            if (string.IsNullOrEmpty(userNo) == false)
            {
                // キャッシュが存在する場合はキャッシュから返す
                return long.Parse(userNo);
            }

            try
            {
                var no = SP_AspNetUsers.GetNo(dbContext, userID);
                session.SetString(SessionKey.UserNo, no.ToString());

                return no;
            }
            catch(Exception ex)
            {
                MailNotify.ExceptionMail(emailSender, "userID: " + userID, ex);
            }

            return null;
        }

        public static ViewModel_AdverEdit SetAdverEdit_DeleteFlg_Search(ISession session, int id)
        {
            var viewModel = session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
            viewModel.SearchWordList.First(x => x.Id == id).DelFlg = DelFlg.削除する;
            session.Set(SessionKey.ViewModel_AdverEdit, viewModel);

            return viewModel;
        }

        public static ViewModel_AdverEdit SetAdverEdit_DeleteFlg_Relation(ISession session, int id)
        {
            var viewModel = session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
            viewModel.RelationWordList.First(x => x.Id == id).DelFlg = DelFlg.削除する;
            session.Set(SessionKey.ViewModel_AdverEdit, viewModel);

            return viewModel;
        }

        //public static void SetAdverEdit_Input(ISession session, InputModel_Adver input)
        //{
        //    var model = session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
        //    model.Adver.Category = input.Category;
        //    model.Adver.AdverName = input.AdverName;
        //    model.Adver.AdverTitle1 = input.AdverTitle1;
        //    model.Adver.AdverTitle2 = input.AdverTitle2;
        //    model.Adver.AdverURL = input.AdverURL;
        //    model.Adver.AdvertisingBudget = input.AdvertisingBudget;

        //    session.Set(SessionKey.ViewModel_AdverEdit, model);
        //}
        
        //public static void SetAdverWordEdit_Input(ISession session, InputModel_AdverWordEdit input)
        //{
        //    //var model = session.Get<InputModel_AdverWordEdit>(SessionKey.InputModel_AdverWordEdit);
        //    var model = new InputModel_AdverWordEdit()
        //    {
        //        WordId = input.WordId,
        //        Word = input.Word,
        //        ClickCost = input.ClickCost,
        //    };

        //    session.Set(SessionKey.InputModel_AdverWordEdit, model);
        //}

        public static void SetAdverRelationEdit_Input(ISession session, InputModel_AdverRelation input)
        {
            var model = session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
            model.Adver.Category = input.Category;
            model.Adver.AdverName = input.AdverName;
            model.Adver.AdverTitle1 = input.AdverTitle1;
            model.Adver.AdverTitle2 = input.AdverTitle2;
            model.Adver.AdverURL = input.AdverURL;
            model.Adver.AdvertisingBudget = input.AdvertisingBudget;

            session.Set(SessionKey.ViewModel_AdverEdit, model);
        }

        public static void SetAdverSearchEdit_Input(ISession session, InputModel_AdverSearch input)
        {
            var model = session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
            model.Adver.Category = input.Category;
            model.Adver.AdverName = input.AdverName;
            model.Adver.AdverTitle1 = input.AdverTitle1;
            model.Adver.AdverTitle2 = input.AdverTitle2;
            model.Adver.AdverURL = input.AdverURL;
            model.Adver.AdvertisingBudget = input.AdvertisingBudget;

            session.Set(SessionKey.ViewModel_AdverEdit, model);
        }

        public static UserSettingProfile GetUserSettingProfile(ISession session,
            ApplicationDbContext dbContext, IMemoryCache cache,
            UserManager<IdentityUser> userManager, IEmailSender emailSender, ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);

            var no = SessionCache.GetUserNo(dbContext, session, emailSender, userId);
            if (no == null)
            {
                return null;
            }
            var userNo = (long)no;

            return SP_UserSetting.SelectProfile(dbContext, userNo)[0];
        }

        public static UserSetting GetUserSetting(ISession session,
            ApplicationDbContext dbContext, IMemoryCache cache, IEmailSender emailSender,
            UserManager<IdentityUser> userManager, ClaimsPrincipal user)
        {
            var backgroundColor = session.GetInt32(SessionKey.BackgroundColor);
            var externalSearchEngine = session.GetInt32(SessionKey.ExternalSearchEngine);

            if (backgroundColor == null || externalSearchEngine == null)
            {
                var userId = userManager.GetUserId(user);

                var no = SessionCache.GetUserNo(dbContext, session, emailSender, userId);
                if (no == null)
                {
                    return new UserSetting()
                    {
                        BackgroundColor = (BackgroundColor)backgroundColor,
                        ExternalSearchEngine = (ExternalSearchEngine)externalSearchEngine
                    };
                }
                var userNo = (long)no;

                var userSetting = SP_UserSetting.Select(dbContext, userNo);

                session.SetInt32(SessionKey.BackgroundColor, (int)userSetting[0].BackgroundColor);
                session.SetInt32(SessionKey.ExternalSearchEngine, (int)userSetting[0].ExternalSearchEngine);

                return userSetting[0];
            }
            else
            {
                return new UserSetting()
                {
                    BackgroundColor = (BackgroundColor)backgroundColor,
                    ExternalSearchEngine = (ExternalSearchEngine)externalSearchEngine
                };
            }
        }

        public static BackgroundColor GetBackgroundColor(ISession session,
            ApplicationDbContext dbContext, IMemoryCache cache, IEmailSender emailSender,
            UserManager<IdentityUser> userManager, ClaimsPrincipal user)
        {
            var backgroundColor = session.GetInt32(SessionKey.BackgroundColor);

            if (backgroundColor == null)
            {
                var userId = userManager.GetUserId(user);

                var no2 = SessionCache.GetUserNo(dbContext, session, emailSender, userId);
                if (no2 == null)
                {
                    return BackgroundColor.White;
                }
                var userNo = (long)no2;

                var userSetting = SP_UserSetting.Select(dbContext, userNo);

                if (userSetting.Count < 1)
                {
                    return BackgroundColor.White;
                }

                session.SetInt32(SessionKey.BackgroundColor, (int)userSetting[0].BackgroundColor);

                return userSetting[0].BackgroundColor;
            }
            else
            {
                return (BackgroundColor)backgroundColor;
            }
        }

        public static ExternalSearchEngine GetExternalSearchEngine(ISession session,
            ApplicationDbContext dbContext, IMemoryCache cache, IEmailSender emailSender,
            long userNo)
        {
            if (userNo == -1)
            {
                // 未ログインユーザーはGoogle固定。
                return ExternalSearchEngine.Google;
            }

            var externalSearchEngine = session.GetInt32(SessionKey.ExternalSearchEngine);

            if (externalSearchEngine == null)
            {
                var userSetting = SP_UserSetting.Select(dbContext, userNo);

                if (userSetting.Count < 1)
                {
                    return ExternalSearchEngine.Google;
                }

                session.SetInt32(SessionKey.ExternalSearchEngine, (int)userSetting[0].ExternalSearchEngine);

                return userSetting[0].ExternalSearchEngine;
            }
            else
            {
                return (ExternalSearchEngine)externalSearchEngine;
            }
        }

        // 編集用のセッション変数を全て削除
        public static void AdverAllRemove(ISession session)
        {
            session.Remove(SessionKey.ViewModel_AdverEdit);
            //session.Remove(SessionKey.AdverCompetingUnitPrice);
            session.Remove(SessionKey.AdverSearchWordEdit);
            session.Remove(SessionKey.AdverRelationWordEdit);
            session.Remove(SessionKey.ScreenTransitionMode);
            //session.Remove(SessionKey.AdverRelationWordSelect_rid);
            //session.Remove(SessionKey.AdverRelationWordSelect_rid);
        }
    }
}
